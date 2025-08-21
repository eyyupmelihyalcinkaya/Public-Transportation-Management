using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GPSService.Models
{
    public class VehicleLocationEvent
    {
        [JsonPropertyName("vehicleId")]
        public string VehicleId { get; set; } = string.Empty;

        [JsonPropertyName("routeId")]
        public int RouteId { get; set; }

        [JsonPropertyName("routeName")]
        public string RouteName { get; set; } = string.Empty;

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("speed")]
        public double Speed { get; set; }

        [JsonPropertyName("heading")]
        public double Heading { get; set; }

        [JsonPropertyName("status")]
        public VehicleStatus Status { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("passengerCount")]
        public int PassengerCount { get; set; }

        [JsonPropertyName("nextStopName")]
        public string? NextStopName { get; set; }

        [JsonPropertyName("distanceToNextStop")]
        public double? DistanceToNextStop { get; set; }

        [JsonPropertyName("estimatedArrivalMinutes")]
        public int? EstimatedArrivalMinutes { get; set; }

        [JsonPropertyName("vehicleType")]
        public VehicleType VehicleType { get; set; }

        // Ek bilgiler
        [JsonPropertyName("isDelayed")]
        public bool IsDelayed { get; set; }

        [JsonPropertyName("delayReason")]
        public string? DelayReason { get; set; }

        [JsonPropertyName("fuelLevel")]
        public double FuelLevel { get; set; } // 0-100 arası

        [JsonPropertyName("isAccessible")]
        public bool IsAccessible { get; set; } // Engelli erişilebilirliği
    }
}
