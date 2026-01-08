using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Models
{
    public class Vehicle
    {
        public string Id { get; set; } // Araç ID'si
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public VehicleType Type { get; set; } // 1= Bus, 2 = Tram, 3 = Metro, 4 = Metrobus
        public string LicensePlate { get; set; } // plaka
        public int Capacity { get; set; } 
        public bool IsActive { get; set; } 

        // simülasyon için ek alanlar

        public VehicleLocation CurrentLocation { get; set; } // araç konumu
        public List<RoutePoint> RoutePoints { get; set; } 
        public int CurrentRoutePointIndex { get; set; } // şu anki rota noktası indeksi
        public double BaseSpeed { get; set; } // temel hız (km/saat)
        public DateTime LastUpdate { get; set; } 

    }

    public enum VehicleType
    {
        Bus = 1,
        Metro = 2,    
        Metrobus = 3,
        Tram = 4
    }
}
