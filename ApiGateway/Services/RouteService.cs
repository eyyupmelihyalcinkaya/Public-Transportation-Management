using ApiGateway.Configuration;
using ApiGateway.Models;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace ApiGateway.Services
{
    public class RouteService : IRouteService
    {
        private readonly GatewaySettings _settings;
        private readonly List<ServiceRoute> _routes;
        private readonly ILogger<RouteService> _logger;

        public RouteService(IOptions<GatewaySettings> settings, ILogger<RouteService> logger)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _routes = InitializeRoutes();
            _logger.LogInformation("RouteService initialized with {RouteCount} routes", _routes.Count);
        }

        private List<ServiceRoute> InitializeRoutes()
        {
            var routes = new List<ServiceRoute>();
            if (_settings.Services == null) return routes;

            // Dynamically create routes from configuration
            foreach (var (serviceName, serviceConfig) in _settings.Services)
            {
                if (serviceConfig.Routes == null) continue;

                foreach (var routeConfig in serviceConfig.Routes)
                {
                    var route = CreateRoute(routeConfig.Path, serviceName, routeConfig.AllowedMethods, routeConfig.RequiresAuth);
                    if (route != null)
                    {
                        routes.Add(route);
                    }
                }
            }

            // Add health check routes manually as they are special
            routes.Add(new ServiceRoute
            {
                Path = "/health",
                ServiceName = "Health",
                TargetUrl = "/health",
                RequiresAuth = false,
                AllowedMethods = new[] { "GET" },
                IsActive = true,
                Pattern = ConvertPathToRegex("/health")
            });
            routes.Add(new ServiceRoute
            {
                Path = "/health/{service}",
                ServiceName = "Health",
                TargetUrl = "/health/{service}",
                RequiresAuth = false,
                AllowedMethods = new[] { "GET" },
                IsActive = true,
                Pattern = ConvertPathToRegex("/health/{service}")
            });

            return routes;
        }

        private ServiceRoute? CreateRoute(string path, string serviceName, string[] methods, bool requiresAuth = true)
        {
            if (!_settings.Services.TryGetValue(serviceName, out var serviceConfig) || string.IsNullOrEmpty(serviceConfig.BaseUrl))
            {
                _logger.LogWarning("Service configuration or BaseUrl not found for service: {ServiceName}", serviceName);
                return null;
            }

            return new ServiceRoute
            {
                Path = path,
                ServiceName = serviceName,
                TargetUrl = serviceConfig.BaseUrl,
                RequiresAuth = requiresAuth,
                AllowedMethods = methods,
                IsActive = serviceConfig.IsActive,
                Pattern = ConvertPathToRegex(path)
            };
        }

        private string ConvertPathToRegex(string path)
        {
            // Convert path parameters to regex pattern
            var pattern = Regex.Replace(path, @"{([a-zA-Z0-9_]+)}", "(?<$1>[^/]+)");
            return $"^{pattern}/?$";
        }

        public ServiceRoute? GetRoute(string path, string method)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(method))
            {
                return null;
            }

            foreach (var route in _routes)
            {
                if (!route.IsActive) continue;

                var regex = new Regex(route.Pattern, RegexOptions.IgnoreCase);
                var match = regex.Match(path);

                if (match.Success && route.AllowedMethods.Contains(method, StringComparer.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Route found for path '{Path}'. Forwarding to {ServiceName}", path, route.ServiceName);

                    // Create a new ServiceRoute object with the resolved path
                    var resolvedPath = route.Path;
                    foreach (var groupName in regex.GetGroupNames())
                    {
                        if (match.Groups.ContainsKey(groupName) && match.Groups[groupName].Success)
                        {
                            resolvedPath = resolvedPath.Replace("{" + groupName + "}", match.Groups[groupName].Value);
                        }
                    }

                    return new ServiceRoute
                    {
                        Path = resolvedPath,
                        ServiceName = route.ServiceName,
                        TargetUrl = route.TargetUrl,
                        RequiresAuth = route.RequiresAuth,
                        AllowedMethods = route.AllowedMethods,
                        IsActive = route.IsActive,
                        Pattern = route.Pattern
                    };
                }
            }

            _logger.LogWarning("No route found for path: {Path} with method: {Method}", path, method);
            return null;
        }

        public bool IsRouteAllowed(string path, string method)
        {
            var route = GetRoute(path, method);
            if (route == null)
            {
                _logger.LogDebug("Route not allowed - No matching route found for path: {Path}, method: {Method}", path, method);
                return false;
            }

            _logger.LogDebug("Route allowed: {Path} ({Method}) - Auth required: {RequiresAuth}", 
                path, method, route.RequiresAuth);
            
            return true;
        }

        public string GetServiceUrl(string serviceName, string path)
        {
            var route = _routes.FirstOrDefault(r => 
                r.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase) &&
                r.Path.Equals(path, StringComparison.OrdinalIgnoreCase));
            
            return route?.TargetUrl ?? string.Empty;
        }

        public List<ServiceRoute> GetAllRoutes()
        {
            return _routes.Where(r => r.IsActive).ToList();
        }
    }
}
