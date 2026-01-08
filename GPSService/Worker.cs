using GPSService.Interfaces;

namespace GPSService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IVehicleLocationSimulator _simulator;
        private readonly IRabbitMqService _rabbitMqService;

        public Worker(
            ILogger<Worker> logger,
            IVehicleLocationSimulator simulator,
            IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _simulator = simulator;
            _rabbitMqService = rabbitMqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("GPS Service Worker başlatılıyor...");

                // RabbitMQ consumer'ını başlat (rota komutlarını dinle)
                await _rabbitMqService.StartConsumingAsync("route-selection-commands", async (message) =>
                {
                    try
                    {
                        var command = System.Text.Json.JsonSerializer.Deserialize<GPSService.Models.RouteSelectionCommand>(message);
                        if (command != null)
                        {
                            await _simulator.HandleRouteSelectionCommandAsync(command);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Rota komutu işlenirken hata: {Message}", message);
                    }
                });

                // Ana simülasyon sistemini başlat
                await _simulator.StartSimulationAsync(stoppingToken);

                _logger.LogInformation("GPS Service Worker başlatıldı ve simülasyon çalışıyor");

                // Servis çalışmaya devam etsin
                while (!stoppingToken.IsCancellationRequested)
                {
                    // Periyodik istatistik loglama
                    try
                    {
                        var stats = await _simulator.GetSimulationStatisticsAsync();
                        _logger.LogInformation(
                            "Simülasyon Durumu - Aktif Rotalar: {ActiveRoutes}, Toplam Araçlar: {TotalVehicles}, " +
                            "Hareket Eden: {MovingVehicles}, Duran: {StoppedVehicles}",
                            stats.ActiveRouteCount,
                            stats.TotalActiveVehicles,
                            stats.TotalMovingVehicles,
                            stats.TotalStoppedVehicles);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "İstatistik alınırken hata");
                    }

                    // 30 saniye bekle
                    await Task.Delay(30000, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("GPS Service Worker durduruluyor...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GPS Service Worker'da beklenmeyen hata");
                throw;
            }
            finally
            {
                try
                {
                    // Simülasyonu temiz şekilde durdur
                    await _simulator.StopSimulationAsync();

                    // RabbitMQ consumer'ını durdur
                    await _rabbitMqService.StopConsumingAsync("route-selection-commands");

                    _logger.LogInformation("GPS Service Worker temiz şekilde durduruldu");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Worker durdurulurken hata");
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GPS Service Worker durduruluyor...");

            try
            {
                await _simulator.StopSimulationAsync();
                _logger.LogInformation("Simülasyon durduruldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Simülasyon durdurulurken hata");
            }

            await base.StopAsync(cancellationToken);
        }
    }
}