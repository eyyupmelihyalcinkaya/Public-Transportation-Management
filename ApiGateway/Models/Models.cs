namespace ApiGateway.Models
{
    public class ServiceRoute
    {
        public string Path { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string TargetUrl { get; set; } = string.Empty;
        public bool RequiresAuth { get; set; } = true;
        public string[] AllowedMethods { get; set; } = new[] { "GET", "POST", "PUT", "DELETE" };
        public int TimeoutSeconds { get; set; } = 30;
        public bool IsActive { get; set; } = true;
        public string Pattern { get; set; } = string.Empty;
    }

    public class ProxyRequest
    {
        public string Method { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public Dictionary<string, string> Headers { get; set; } = new();
        public object? Body { get; set; }
        public int TimeoutSeconds { get; set; } = 30;
    }

    public class ProxyResponse
    {
        public int StatusCode { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new();
        public object? Body { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public TimeSpan ResponseTime { get; set; }
        public string? ContentType { get; set; }
    }

    public class HealthCheckResponse
    {
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public Dictionary<string, ServiceHealth> Services { get; set; } = new();
        public TimeSpan Uptime { get; set; }
    }

    public class ServiceHealth
    {
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public TimeSpan ResponseTime { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime LastChecked { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
