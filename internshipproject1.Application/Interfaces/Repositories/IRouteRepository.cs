using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;
using MediatR;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IRouteRepository : IGenericRepository<RouteToCreate>
    {
        Task<RouteToCreate> GetByIdAsync(int id,CancellationToken cancellationToken);
        Task<RouteToCreate> GetByRouteNameAsync(string routeName, CancellationToken cancellationToken);
        Task<RouteToCreate> AddAsync(RouteToCreate route, CancellationToken cancellationToken);
        Task<RouteToCreate> UpdateAsync(RouteToCreate route, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> RouteExistsAsync(string routeName, CancellationToken cancellationToken);
        Task<bool> RouteExistByIdAsync(int routeId,CancellationToken cancellationToken);
        Task<int> TotalRoutesCount(CancellationToken cancellationToken);
    }
}
