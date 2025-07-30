namespace ApiGateway.Configuration
{
    public class GatewaySettings
    {
        public Dictionary<string, ServiceConfiguration> Services { get; private init; } = new();
        public MaintenanceModeSettings? MaintenanceMode { get; set; }
        public string ApiKey { get; set; } = string.Empty;
        public string JwtSecret { get; set; } = string.Empty;
        public int RateLimitPerMinute { get; set; } = 100;
        public bool EnableLogging { get; set; } = true;
        public string LogLevel { get; set; } = "Information";
    }

    public class ServiceConfiguration
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string HealthEndpoint { get; set; } = "/health";
        public int TimeoutSeconds { get; set; } = 30;
        public int RetryCount { get; set; } = 3;
        public bool IsActive { get; set; } = true;
        public List<RouteConfiguration> Routes { get; set; } = new();
    }

    public class RouteConfiguration
    {
        public string Path { get; set; } = string.Empty;
        public string[] AllowedMethods { get; set; } = { "GET" };
        public bool RequiresAuth { get; set; } = true;
    }

    public class MaintenanceModeSettings
    {
        public bool IsEnabled { get; set; } = false;
        public string? Message { get; set; }
    }
}