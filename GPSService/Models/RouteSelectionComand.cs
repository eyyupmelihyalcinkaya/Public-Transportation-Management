using System.Text.Json.Serialization;

namespace GPSService.Models
{
    public class RouteSelectionCommand
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;

        [JsonPropertyName("routeId")]
        public int RouteId { get; set; }

        [JsonPropertyName("routeName")]
        public string? RouteName { get; set; }

        [JsonPropertyName("userId")]
        public string? UserId { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("ttlSeconds")]
        public int? TtlSeconds { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Action))
                return false;

            if (RouteId <= 0)
                return false;

            var validActions = new[] { "START", "STOP" };
            if (!validActions.Contains(Action.ToUpper()))
                return false;

            if (TtlSeconds.HasValue && TtlSeconds > 0)
            {
                var expiryTime = Timestamp.AddSeconds(TtlSeconds.Value);
                if (DateTime.UtcNow > expiryTime)
                    return false;
            }

            return true;
        }

        public string ToLogMessage()
        {
            return $"RouteCommand: {Action} Route {RouteId} ({RouteName ?? "Unknown"}) " +
                   $"by User {UserId ?? "System"} at {Timestamp:yyyy-MM-dd HH:mm:ss} " +
                   $"[RequestId: {RequestId}]";
        }
    }
}