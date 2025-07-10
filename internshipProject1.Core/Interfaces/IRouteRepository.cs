using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipProject1.Core.Interfaces
{
    internal interface IRouteRepository
    {

        Task<RouteToCreate> GetByIdAsync(int id);
        Task<IEnumerable<RouteToCreate>> GetAllAsync();
        Task<RouteToCreate> AddAsync(RouteToCreate route);
        Task<RouteToCreate> UpdateAsync(RouteToCreate route);
        Task DeleteAsync(int id);
        Task<IEnumerable<Stop>> GetRouteStopAsync(int routeId);

    }
}
