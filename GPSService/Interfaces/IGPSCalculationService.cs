using GPSService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Interfaces
{
    public interface IGPSCalculationService
    {
        double CalculateDistance(double lat1,double lon1, double lat2, double lon2);
        double CalculateBearing(double lat1, double lon1, double lat2, double lon2);
        (double Latitude, double Longitude) CalculateDestination(double lat, double lon, double bearing, double distanceKm);
        bool IsPointNearRoute(double pointLat, double pointLon, List<RoutePoint> routePoints, double toleranceKm = 0.1);
    }
}
