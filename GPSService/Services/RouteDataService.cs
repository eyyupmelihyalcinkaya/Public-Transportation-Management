using GPSService.Interfaces;
using GPSService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPSService.Services
{
    public class RouteDataService : IRouteDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RouteDataService> _logger;
        public RouteDataService(HttpClient httpClient, IConfiguration configuration, ILogger<RouteDataService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;


            //Rotayı backendden çekebilmek için gerekli base URL ayarlarını yaptığım yer. 
            // Eğer API Key varsa onu da header'a ekliyorum, yoksa uyarı veriyorum.
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7009";
            _httpClient.BaseAddress = new Uri(apiBaseUrl);

            var apiKey = _configuration["ApiSettings:ApiKey"];
            if (!string.IsNullOrEmpty(apiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            }
            else
            {
                _logger.LogWarning("API Key is not configured. Some features may not work as expected.");
            }
        }

        public async Task<IEnumerable<Route>> GetActiveRoutesAsync()
        {
            try
            {
                _logger.LogDebug("Fetching active routes from API.");
                var response = await _httpClient.GetAsync("api/routes/");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var routes = JsonSerializer.Deserialize<List<ApiRoute>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return routes?.Select(MapToRoute) ?? Enumerable.Empty<Route>();
                }
                else
                {
                    _logger.LogWarning("API'den rotalar alınamadı. Status: {StatusCode}", response.StatusCode);
                    return Enumerable.Empty<Route>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching active routes.");
                return Enumerable.Empty<Route>();
            }
        }

        public async Task<Route> GetRouteAsync(int routeId)
        {
            try 
            {
                _logger.LogDebug("Fetching route with ID {RouteId} from API.", routeId);
                var response = await _httpClient.GetAsync($"api/routes/{routeId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var apiRoute = JsonSerializer.Deserialize<ApiRoute>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return apiRoute != null ? MapToRoute(apiRoute) : null;
                }
                else
                {
                    _logger.LogWarning("Rota {RouteId} bulunamadı. Status: {StatusCode}", routeId, response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching route with ID {RouteId}.", routeId);
                return null;
            }
        }

        public async Task<IEnumerable<RoutePoint>> GetRoutePointsAsync(int routeId)
        {
            try
            {
                _logger.LogDebug("Rota noktaları alınıyor. Route Id: {RouteId}", routeId);
                var response = await _httpClient.GetAsync($"api/routes/{routeId}/stops");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var stops = JsonSerializer.Deserialize<List<ApiStop>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return stops?.Select(MapToRoutePoint) ?? Enumerable.Empty<RoutePoint>();
                }
                else 
                {
                    _logger.LogWarning("Rota {RouteId} noktaları alınamadı. Status: {StatusCode}", routeId, response.StatusCode);
                    return null;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Rota {RouteId} noktaları alınırken hata oluştu", routeId);
                return null;
            }
        }

        public async Task<IEnumerable<Stop>> GetRouteStopsAsync(int routeId)
        {
            try
            {
                _logger.LogDebug("Rota {RouteId} durakları alınıyor...", routeId);

                var response = await _httpClient.GetAsync($"/api/routes/{routeId}/stops");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var apiStops = JsonSerializer.Deserialize<List<ApiStop>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return apiStops?.Select(MapToStop) ?? Enumerable.Empty<Stop>();
                }
                else
                {
                    _logger.LogWarning("Rota {RouteId} durakları alınamadı. Status: {StatusCode}", routeId, response.StatusCode);
                    return Enumerable.Empty<Stop>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rota {RouteId} durakları alınırken hata oluştu", routeId);
                return Enumerable.Empty<Stop>();
            }
        }


        private Route MapToRoute(ApiRoute apiRoute)
        {
            return new Route
            {
                Id = apiRoute.Id,
                Name = apiRoute.Name,
                Description = apiRoute.Description,
                IsActive = true
            };
        }

        private RoutePoint MapToRoutePoint(ApiStop apiStop)
        {
            return new RoutePoint
            {
                Latitude = apiStop.Latitude,
                Longitude = apiStop.Longitude,
                Order = apiStop.Order ?? 0,
                StopName = apiStop.Name,
                IsStop = true,
                StopDurationSeconds = 30, // Varsayılan durak süresi
                SpeedLimit = 30 // Varsayılan hız limiti
            };
        }

        private Stop MapToStop(ApiStop apiStop)
        {
            return new Stop
            {
                Id = apiStop.Id,
                Name = apiStop.Name,
                Latitude = apiStop.Latitude,
                Longitude = apiStop.Longitude
            };
        }

    }


    public class ApiRoute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class  ApiStop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? Order { get; set; }
    }
}
