using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;
using internshipproject1.Application.Interfaces.Repositories;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IRouteStopRepository : IGenericRepository<Domain.Entities.RouteStop>
    {
        Task<RouteStop> GetByIdAsync(int id);
        Task<RouteStop> GetByRouteIdAndStopIdAsync(int routeId, int stopId);
        Task<RouteStop> AddAsync(RouteStop routeStop);
        Task<RouteStop> UpdateAsync(RouteStop routeStop);
        Task DeleteAsync(int id);
        Task<bool> RouteStopExistsAsync(int routeId, int stopId);
        Task<IReadOnlyList<RouteStop>> GetAllByRouteIdAsync(int routeId);
        Task<IReadOnlyList<RouteStop>> GetAllByStopIdAsync(int stopId);
        Task<IReadOnlyList<RouteStop>> GetAllAsync();

    }
}
