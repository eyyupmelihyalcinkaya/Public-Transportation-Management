using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Models
{
    public class SimulationStatistics
    {
        public int ActiveRouteCount { get; set; }
        public int TotalActiveVehicles { get; set; }
        public int TotalMovingVehicles { get; set; }
        public int TotalStoppedVehicles { get; set; }
        public int TotalDelayedVehicles { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public TimeSpan SystemUptime { get; set; }
        public Dictionary<int, RouteStatistics> RouteStatistics { get; set; } = new();
        public Dictionary<VehicleType, int> VehicleTypeDistribution { get; set; } = new();
        public double AverageSpeed { get; set; }
        public int TotalPassengers { get; set; }
        public double SystemLoadPercentage { get; set; }
    }

    public class RouteStatistics
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public int VehicleCount { get; set; }
        public int ActiveVehicleCount { get; set; }
        public double AverageSpeed { get; set; }
        public int TotalPassengers { get; set; }
        public DateTime SimulationStartTime { get; set; }
        public VehicleStatus[] VehicleStatuses { get; set; } = Array.Empty<VehicleStatus>();
    }
}
