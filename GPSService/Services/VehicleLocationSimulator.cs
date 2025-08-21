using GPSService.Interfaces;
using GPSService.Models;
using System.Collections.Concurrent;

namespace GPSService.Services
{
    public class VehicleLocationSimulator : IVehicleLocationSimulator
    {
        private readonly IRouteDataService _routeDataService;
        private readonly IGPSCalculationService _gpsCalculationService;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ILogger<VehicleLocationSimulator> _logger;
        private readonly ConcurrentDictionary<string, Vehicle> _activeVehicles;
        private readonly ConcurrentDictionary<int, CancellationTokenSource> _routeSimulations;
        private readonly ConcurrentDictionary<int, DateTime> _routeStartTimes;
        private readonly Random _random;
        private readonly DateTime _systemStartTime;
        private CancellationTokenSource? _globalCancellationTokenSource;

        public VehicleLocationSimulator(
            IRouteDataService routeDataService,
            IGPSCalculationService gpsCalculationService,
            IRabbitMqService rabbitMqService,
            ILogger<VehicleLocationSimulator> logger)
        {
            _routeDataService = routeDataService;
            _gpsCalculationService = gpsCalculationService;
            _rabbitMqService = rabbitMqService;
            _logger = logger;
            _activeVehicles = new ConcurrentDictionary<string, Vehicle>();
            _routeSimulations = new ConcurrentDictionary<int, CancellationTokenSource>();
            _routeStartTimes = new ConcurrentDictionary<int, DateTime>();
            _random = new Random();
            _systemStartTime = DateTime.UtcNow;
        }

        public async Task StartSimulationAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Genel simülasyon sistemi başlatılıyor...");
                
                _globalCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                
                // Aktif rotaları al ve simülasyonları başlat
                var activeRoutes = await _routeDataService.GetActiveRoutesAsync();
                
                var startTasks = activeRoutes.Select(route => StartRouteSimulation(route.Id));
                await Task.WhenAll(startTasks);
                
                _logger.LogInformation("Genel simülasyon sistemi başlatıldı. {RouteCount} rota aktif.", activeRoutes.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel simülasyon başlatılırken hata oluştu");
                throw;
            }
        }

        public async Task StopSimulationAsync()
        {
            _logger.LogInformation("Genel simülasyon sistemi durduruluyor...");

            // Tüm rota simülasyonlarını durdur
            var stopTasks = _routeSimulations.Keys.Select(routeId => StopRouteSimulation(routeId));
            await Task.WhenAll(stopTasks);

            // Global cancellation token'ı iptal et
            _globalCancellationTokenSource?.Cancel();
            _globalCancellationTokenSource?.Dispose();
            _globalCancellationTokenSource = null;

            _activeVehicles.Clear();
            _routeStartTimes.Clear();
            
            _logger.LogInformation("Genel simülasyon sistemi durduruldu");
        }

        public async Task<IEnumerable<Vehicle>> GetActiveVehicleAsync()
        {
            return await Task.FromResult(_activeVehicles.Values.ToList());
        }

        public async Task UpdateVehicleLocationAsync(Vehicle vehicle)
        {
            if (vehicle?.RoutePoints == null || !vehicle.RoutePoints.Any())
            {
                _logger.LogWarning("Araç {VehicleId} için rota noktaları bulunamadı", vehicle?.Id);
                return;
            }

            try
            {
                await UpdateVehiclePosition(vehicle);

                // Konum güncellemesini RabbitMQ'ya gönder
                var locationEvent = CreateVehicleLocationEvent(vehicle);
                await _rabbitMqService.PublishAsync("vehicle-location-updates", locationEvent);

                _logger.LogDebug("Araç {VehicleId} konumu güncellendi", vehicle.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç {VehicleId} konumu güncellenirken hata", vehicle.Id);
            }
        }

        public async Task<VehicleLocation> GenerateNextLocationAsync(Vehicle vehicle)
        {
            try
            {
                if (vehicle?.RoutePoints == null || !vehicle.RoutePoints.Any())
                {
                    _logger.LogWarning("Araç {VehicleId} için rota noktaları bulunamadı", vehicle?.Id);
                    return null;
                }

                var currentPoint = vehicle.RoutePoints[vehicle.CurrentRoutePointIndex];
                var nextIndex = (vehicle.CurrentRoutePointIndex + 1) % vehicle.RoutePoints.Count;
                var nextPoint = vehicle.RoutePoints[nextIndex];

                // İki nokta arasında ilerleme hesapla
                var progress = _random.NextDouble() * 0.1; // %10'luk ilerleme
                var lat = currentPoint.Latitude + (nextPoint.Latitude - currentPoint.Latitude) * progress;
                var lon = currentPoint.Longitude + (nextPoint.Longitude - currentPoint.Longitude) * progress;

                // Yön hesapla
                var bearing = _gpsCalculationService.CalculateBearing(
                    currentPoint.Latitude, currentPoint.Longitude,
                    nextPoint.Latitude, nextPoint.Longitude);

                var distance = _gpsCalculationService.CalculateDistance(lat, lon, nextPoint.Latitude, nextPoint.Longitude);

                return new VehicleLocation
                {
                    VehicleId = vehicle.Id,
                    RouteId = vehicle.RouteId,
                    Latitude = lat,
                    Longitude = lon,
                    Speed = vehicle.BaseSpeed + _random.Next(-10, 10),
                    Heading = bearing,
                    Status = distance < 0.01 ? VehicleStatus.AtStation : VehicleStatus.Moving,
                    Timestamp = DateTime.UtcNow,
                    PassengerCount = vehicle.CurrentLocation?.PassengerCount ?? _random.Next(5, 50),
                    NextStopName = nextPoint.StopName,
                    DistanceToNextStop = distance,
                    EstimatedArrivalMinutes = (int)(distance / (vehicle.BaseSpeed / 60))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç {VehicleId} için yeni konum oluşturulurken hata", vehicle?.Id);
                return null;
            }
        }

        public async Task StartRouteSimulation(int routeId)
        {
            try
            {
                _logger.LogInformation("Rota {RouteId} simülasyonu başlatılıyor...", routeId);

                // Eğer bu rota için zaten bir simülasyon varsa durdur
                if (_routeSimulations.ContainsKey(routeId))
                {
                    await StopRouteSimulation(routeId);
                }

                // Rota bilgilerini al
                var route = await _routeDataService.GetRouteAsync(routeId);
                if (route == null)
                {
                    _logger.LogWarning("Rota {RouteId} bulunamadı", routeId);
                    return;
                }

                var routePoints = await _routeDataService.GetRoutePointsAsync(routeId);
                if (routePoints == null || !routePoints.Any())
                {
                    _logger.LogWarning("Rota {RouteId} için nokta bulunamadı", routeId);
                    return;
                }

                // Simülasyon için CancellationTokenSource oluştur
                var cts = new CancellationTokenSource();
                if (_globalCancellationTokenSource != null)
                {
                    cts = CancellationTokenSource.CreateLinkedTokenSource(_globalCancellationTokenSource.Token);
                }

                _routeSimulations[routeId] = cts;
                _routeStartTimes[routeId] = DateTime.UtcNow;

                // Rota için araçları oluştur
                var vehicleCount = _random.Next(2, 5);
                var vehicles = CreateVehiclesForRoute(route, routePoints.ToList(), vehicleCount);

                // Her araç için simülasyon görevini başlat
                _ = Task.Run(async () =>
                {
                    var tasks = vehicles.Select(vehicle => SimulateVehicleMovementAsync(vehicle, cts.Token));
                    try
                    {
                        await Task.WhenAll(tasks);
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogInformation("Rota {RouteId} simülasyonu iptal edildi", routeId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Rota {RouteId} simülasyonu sırasında hata", routeId);
                    }
                }, cts.Token);

                _logger.LogInformation("Rota {RouteId} simülasyonu başlatıldı. {VehicleCount} araç aktif.", routeId, vehicleCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rota {RouteId} simülasyonu başlatılırken hata", routeId);
                throw;
            }
        }

        public async Task StopRouteSimulation(int routeId)
        {
            try
            {
                if (_routeSimulations.TryRemove(routeId, out var cts))
                {
                    cts.Cancel();
                    cts.Dispose();
                    
                    // Bu rotadaki araçları kaldır
                    var vehiclesToRemove = _activeVehicles.Values
                        .Where(v => v.RouteId == routeId)
                        .Select(v => v.Id)
                        .ToList();

                    foreach (var vehicleId in vehiclesToRemove)
                    {
                        _activeVehicles.TryRemove(vehicleId, out _);
                    }

                    _routeStartTimes.TryRemove(routeId, out _);

                    _logger.LogInformation("Rota {RouteId} simülasyonu durduruldu. {VehicleCount} araç kaldırıldı.", 
                        routeId, vehiclesToRemove.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rota {RouteId} simülasyonu durdurulurken hata", routeId);
            }
        }

        public bool IsRouteSimulationActive(int routeId)
        {
            return _routeSimulations.ContainsKey(routeId) && 
                   !_routeSimulations[routeId].Token.IsCancellationRequested;
        }

        public async Task<IEnumerable<Vehicle>> GetRouteVehiclesAsync(int routeId)
        {
            var routeVehicles = _activeVehicles.Values
                .Where(v => v.RouteId == routeId)
                .ToList();

            return await Task.FromResult(routeVehicles);
        }

        public int GetActiveRouteCount()
        {
            return _routeSimulations.Count(kvp => !kvp.Value.Token.IsCancellationRequested);
        }

        public async Task<int> GetTotalActiveVehicleCountAsync()
        {
            return await Task.FromResult(_activeVehicles.Count);
        }

        public async Task<SimulationStatistics> GetSimulationStatisticsAsync()
        {
            var vehicles = _activeVehicles.Values.ToList();
            var routeStats = new Dictionary<int, RouteStatistics>();

            // Rota istatistiklerini hesapla
            foreach (var routeGroup in vehicles.GroupBy(v => v.RouteId))
            {
                var routeVehicles = routeGroup.ToList();
                var route = await _routeDataService.GetRouteAsync(routeGroup.Key);

                routeStats[routeGroup.Key] = new RouteStatistics
                {
                    RouteId = routeGroup.Key,
                    RouteName = route?.Name ?? "Bilinmeyen Rota",
                    VehicleCount = routeVehicles.Count,
                    ActiveVehicleCount = routeVehicles.Count(v => v.IsActive),
                    AverageSpeed = routeVehicles.Average(v => v.CurrentLocation?.Speed ?? 0),
                    TotalPassengers = routeVehicles.Sum(v => v.CurrentLocation?.PassengerCount ?? 0),
                    SimulationStartTime = _routeStartTimes.GetValueOrDefault(routeGroup.Key, DateTime.UtcNow),
                    VehicleStatuses = routeVehicles.Select(v => v.CurrentLocation?.Status ?? VehicleStatus.Stopped).ToArray()
                };
            }

            var vehicleTypeDistribution = vehicles
                .GroupBy(v => v.Type)
                .ToDictionary(g => g.Key, g => g.Count());

            return await Task.FromResult(new SimulationStatistics
            {
                ActiveRouteCount = GetActiveRouteCount(),
                TotalActiveVehicles = vehicles.Count,
                TotalMovingVehicles = vehicles.Count(v => v.CurrentLocation?.Status == VehicleStatus.Moving),
                TotalStoppedVehicles = vehicles.Count(v => v.CurrentLocation?.Status == VehicleStatus.Stopped),
                TotalDelayedVehicles = vehicles.Count(v => v.CurrentLocation?.Status == VehicleStatus.Delayed),
                LastUpdateTime = DateTime.UtcNow,
                SystemUptime = DateTime.UtcNow - _systemStartTime,
                RouteStatistics = routeStats,
                VehicleTypeDistribution = vehicleTypeDistribution,
                AverageSpeed = vehicles.Any() ? vehicles.Average(v => v.CurrentLocation?.Speed ?? 0) : 0,
                TotalPassengers = vehicles.Sum(v => v.CurrentLocation?.PassengerCount ?? 0),
                SystemLoadPercentage = CalculateSystemLoad()
            });
        }

        public async Task<Vehicle?> GetVehicleAsync(string vehicleId)
        {
            _activeVehicles.TryGetValue(vehicleId, out var vehicle);
            return await Task.FromResult(vehicle);
        }

        public async Task HandleRouteSelectionCommandAsync(RouteSelectionCommand command)
        {
            try
            {
                if (!command.IsValid())
                {
                    _logger.LogWarning("Geçersiz rota komutu alındı: {Command}", command.ToLogMessage());
                    return;
                }

                _logger.LogInformation("Rota komutu işleniyor: {Command}", command.ToLogMessage());

                switch (command.Action.ToUpper())
                {
                    case "START":
                        await StartRouteSimulation(command.RouteId);
                        break;
                    case "STOP":
                        await StopRouteSimulation(command.RouteId);
                        break;
                    default:
                        _logger.LogWarning("Bilinmeyen rota komutu: {Action}", command.Action);
                        break;
                }

                _logger.LogInformation("Rota komutu başarıyla işlendi: {Command}", command.ToLogMessage());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rota komutu işlenirken hata: {Command}", command.ToLogMessage());
            }
        }

        private List<Vehicle> CreateVehiclesForRoute(Route route, List<RoutePoint> routePoints, int vehicleCount)
        {
            var vehicles = new List<Vehicle>();

            for (int i = 0; i < vehicleCount; i++)
            {
                var vehicleId = $"vehicle_{route.Id}_{i + 1}_{Guid.NewGuid().ToString("N")[..8]}";
                var startPointIndex = _random.Next(0, routePoints.Count);
                var startPoint = routePoints[startPointIndex];

                var vehicle = new Vehicle
                {
                    Id = vehicleId,
                    RouteId = route.Id,
                    RouteName = route.Name,
                    Type = (VehicleType)_random.Next(1, 5),
                    LicensePlate = GenerateRandomPlate(),
                    Capacity = _random.Next(30, 100),
                    IsActive = true,
                    CurrentLocation = new VehicleLocation
                    {
                        VehicleId = vehicleId,
                        RouteId = route.Id,
                        Latitude = startPoint.Latitude,
                        Longitude = startPoint.Longitude,
                        Speed = 0,
                        Heading = 0,
                        Status = VehicleStatus.Stopped,
                        Timestamp = DateTime.UtcNow,
                        PassengerCount = _random.Next(0, 10)
                    },
                    RoutePoints = routePoints,
                    CurrentRoutePointIndex = startPointIndex,
                    BaseSpeed = _random.Next(25, 45),
                    LastUpdate = DateTime.UtcNow
                };

                vehicles.Add(vehicle);
                _activeVehicles[vehicleId] = vehicle;
            }

            return vehicles;
        }

        private async Task SimulateVehicleMovementAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UpdateVehicleLocationAsync(vehicle);

                    // Simülasyon hızını ayarla
                    var delay = _random.Next(2000, 8000); // 2-8 saniye
                    await Task.Delay(delay, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug("Araç {VehicleId} simülasyonu iptal edildi", vehicle.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç {VehicleId} simülasyonu sırasında hata", vehicle.Id);
            }
        }

        private async Task UpdateVehiclePosition(Vehicle vehicle)
        {
            if (vehicle.RoutePoints == null || !vehicle.RoutePoints.Any())
                return;

            var currentPoint = vehicle.RoutePoints[vehicle.CurrentRoutePointIndex];
            var nextIndex = (vehicle.CurrentRoutePointIndex + 1) % vehicle.RoutePoints.Count;
            var nextPoint = vehicle.RoutePoints[nextIndex];

            // Mevcut konumdan sonraki noktaya doğru hareket et
            var distance = _gpsCalculationService.CalculateDistance(
                vehicle.CurrentLocation.Latitude, vehicle.CurrentLocation.Longitude,
                nextPoint.Latitude, nextPoint.Longitude);

            // Eğer sonraki noktaya çok yakınsa, o noktaya geç
            if (distance < 0.01) // 10 metre
            {
                vehicle.CurrentRoutePointIndex = nextIndex;
                vehicle.CurrentLocation.Status = VehicleStatus.AtStation;
                
                // Durakta bekle
                await Task.Delay(_random.Next(5000, 15000));
                
                vehicle.CurrentLocation.Status = VehicleStatus.Moving;
            }
            else
            {
                // Sonraki noktaya doğru hareket et
                var bearing = _gpsCalculationService.CalculateBearing(
                    vehicle.CurrentLocation.Latitude, vehicle.CurrentLocation.Longitude,
                    nextPoint.Latitude, nextPoint.Longitude);

                var speedKmh = vehicle.BaseSpeed + _random.Next(-10, 10);
                var speedMs = speedKmh / 3.6;
                var moveDistanceKm = (speedMs * 3) / 1000; // 3 saniyede gidilecek mesafe

                var (newLat, newLon) = _gpsCalculationService.CalculateDestination(
                    vehicle.CurrentLocation.Latitude,
                    vehicle.CurrentLocation.Longitude,
                    bearing,
                    moveDistanceKm);

                vehicle.CurrentLocation.Latitude = newLat;
                vehicle.CurrentLocation.Longitude = newLon;
                vehicle.CurrentLocation.Speed = speedKmh;
                vehicle.CurrentLocation.Heading = bearing;
                vehicle.CurrentLocation.Status = VehicleStatus.Moving;
                vehicle.CurrentLocation.Timestamp = DateTime.UtcNow;
                vehicle.CurrentLocation.NextStopName = nextPoint.StopName;
                vehicle.CurrentLocation.DistanceToNextStop = distance;
                vehicle.CurrentLocation.EstimatedArrivalMinutes = (int)(distance / speedKmh * 60);

                vehicle.LastUpdate = DateTime.UtcNow;
            }
        }

        private VehicleLocationEvent CreateVehicleLocationEvent(Vehicle vehicle)
        {
            return new VehicleLocationEvent
            {
                VehicleId = vehicle.Id,
                RouteId = vehicle.RouteId,
                RouteName = vehicle.RouteName,
                Latitude = vehicle.CurrentLocation.Latitude,
                Longitude = vehicle.CurrentLocation.Longitude,
                Speed = vehicle.CurrentLocation.Speed,
                Heading = vehicle.CurrentLocation.Heading,
                Status = vehicle.CurrentLocation.Status,
                Timestamp = vehicle.CurrentLocation.Timestamp,
                PassengerCount = vehicle.CurrentLocation.PassengerCount,
                NextStopName = vehicle.CurrentLocation.NextStopName,
                DistanceToNextStop = vehicle.CurrentLocation.DistanceToNextStop,
                EstimatedArrivalMinutes = vehicle.CurrentLocation.EstimatedArrivalMinutes,
                VehicleType = vehicle.Type,
                IsDelayed = vehicle.CurrentLocation.Status == VehicleStatus.Delayed,
                DelayReason = vehicle.CurrentLocation.Status == VehicleStatus.Delayed ? "Trafik yoğunluğu" : null,
                FuelLevel = _random.Next(20, 100),
                IsAccessible = _random.NextDouble() > 0.3
            };
        }

        private string GenerateRandomPlate()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numbers = "0123456789";
            
            return $"{letters[_random.Next(letters.Length)]}{letters[_random.Next(letters.Length)]}" +
                   $"{numbers[_random.Next(numbers.Length)]}{numbers[_random.Next(numbers.Length)]}" +
                   $"{letters[_random.Next(letters.Length)]}{letters[_random.Next(letters.Length)]}";
        }

        private double CalculateSystemLoad()
        {
            var totalVehicles = _activeVehicles.Count;
            var activeRoutes = GetActiveRouteCount();
            
            // Basit yük hesaplaması: araç sayısı ve aktif rota sayısına göre
            var load = (totalVehicles * 2 + activeRoutes * 5) / 100.0 * 100;
            return Math.Min(load, 100.0);
        }
    }
}