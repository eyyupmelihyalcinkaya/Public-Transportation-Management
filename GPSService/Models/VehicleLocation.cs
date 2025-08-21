using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Models
{
    public class VehicleLocation
    {
        public string VehicleId { get; set; }
        public int RouteId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Speed { get; set; }
        public double Heading { get; set; } // derece
        public VehicleStatus Status { get; set; }
        public DateTime Timestamp { get; set; } // konumun alındığı zaman
        public int PassengerCount { get; set; } // araçtaki yolcu sayısı
        public string? NextStopName { get; set; } // bir sonraki durak adı
        public double? DistanceToNextStop { get; set; } // bir sonraki durağa olan mesafe
        public int? EstimatedArrivalMinutes { get; set; } // bir sonraki durağa tahmini varış süresi
    }

    public enum  VehicleStatus
    {
        Moving = 1,
        Stopped = 2,
        AtStation = 3,
        OutOfService = 4,
        Delayed = 5,
    }
}
