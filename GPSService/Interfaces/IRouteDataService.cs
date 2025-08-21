using GPSService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Interfaces
{
    public interface IRouteDataService
    {
        Task<IEnumerable<Route>> GetActiveRoutesAsync();
        Task<Route> GetRouteAsync(int routeId);
        Task<IEnumerable<RoutePoint>> GetRoutePointsAsync(int routeId);
        Task<IEnumerable<Stop>> GetRouteStopsAsync(int routeId);
    }
}
