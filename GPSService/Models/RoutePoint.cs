using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Models
{
    public class RoutePoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Order { get; set; } 
        public string? StopName { get; set; } 
        public bool IsStop { get; set; } 
        public int? StopDurationSeconds { get; set; } // durakta bekleme saniyesi
        public double? SpeedLimit { get; set; } // hız limiti
    }
}
