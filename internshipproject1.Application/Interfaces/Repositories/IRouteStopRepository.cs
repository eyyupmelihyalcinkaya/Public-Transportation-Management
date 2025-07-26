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
        Task<RouteStop> GetByIdAsync(int id,CancellationToken cancellationToken);
        Task<RouteStop> GetByRouteIdAndStopIdAsync(int routeId, int stopId, CancellationToken cancellationToken);
        Task<RouteStop> AddAsync(RouteStop routeStop, CancellationToken cancellationToken);
        Task<RouteStop> UpdateAsync(RouteStop routeStop, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> RouteStopExistsAsync(int routeId, int stopId, CancellationToken cancellationToken);
        Task<IReadOnlyList<RouteStop>> GetAllByRouteIdAsync(int routeId, CancellationToken cancellationToken);
        Task<IReadOnlyList<RouteStop>> GetAllByStopIdAsync(int stopId, CancellationToken cancellationToken);
        Task<IReadOnlyList<RouteStop>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> TotalRouteStopsCount(CancellationToken cancellationToken);

    }
}
