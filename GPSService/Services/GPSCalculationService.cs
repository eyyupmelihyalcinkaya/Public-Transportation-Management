using GPSService.Interfaces;
using GPSService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Services
{
    public class GPSCalculationService : IGPSCalculationService
    {
        private const double EarthRadiusKm = 6371.0;

        public double CalculateBearing(double lat1, double lon1, double lat2, double lon2)
        {
            // iki koordinat arasındaki yönü (bearing) hesaplıyorum.

            var dLon = DegreesToRadians(lon2 - lon1);
            var lat1Rad = DegreesToRadians(lat1);
            var lat2Rad = DegreesToRadians(lat2);

            var y = Math.Sin(dLon) * Math.Cos(lat2Rad);
            var x = Math.Cos(lat1Rad) * Math.Sin(lat2Rad) -
                    Math.Sin(lat1Rad) * Math.Cos(lat2Rad) * Math.Cos(dLon);

            var bearing = Math.Atan2(y, x);
            return (bearing + 360) % 360; // 0-360 aralığında döndürür.

        }

        public (double Latitude, double Longitude) CalculateDestination(double lat, double lon, double bearing, double distanceKm)
        {
            // başlangıç noktası, yön açısı ve mesafe verildiğinde hedef koordinatları döndürüyo ~spherical trigonometry.
            
            var bearingRad = DegreesToRadians(bearing);
            var latRad = DegreesToRadians(lat);
            var lonRad = DegreesToRadians(lon);

            var newLatRad = Math.Asin(Math.Sin(latRad) * Math.Cos(distanceKm / EarthRadiusKm) +
                                     Math.Cos(latRad) * Math.Sin(distanceKm / EarthRadiusKm) * Math.Cos(bearingRad));

            var newLonRad = lonRad + Math.Atan2(Math.Sin(bearingRad) * Math.Sin(distanceKm / EarthRadiusKm) * Math.Cos(latRad),
                                                Math.Cos(distanceKm / EarthRadiusKm) - Math.Sin(latRad) * Math.Sin(newLatRad));

            return (RadiansToDegrees(newLatRad), RadiansToDegrees(newLonRad));
        }

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Haversine formülü ile iki nokta arasındaki açıyı hesaplıyorum

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = EarthRadiusKm * c;
            return distance;
        }

        public bool IsPointNearRoute(double pointLat, double pointLon, List<RoutePoint> routePoints, double toleranceKm = 0.1)
        {
            return routePoints.Any(rp => CalculateDistance(pointLat, pointLon, rp.Latitude, rp.Longitude) <= toleranceKm);
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        private static double RadiansToDegrees(double radians)
        {
            return radians * 180.0 / Math.PI;
        }
    }
}
