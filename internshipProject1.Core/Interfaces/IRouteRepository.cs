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

        Task<myRoute> GetByIdAsync(int id);
        Task<IEnumerable<myRoute>> GetAllAsync();
        Task<myRoute> AddAsync(myRoute route);
        Task<myRoute> UpdateAsync(myRoute route);
        Task DeleteAsync(int id);
        Task<IEnumerable<Stop>> GetRouteStopAsync(int routeId);

    }
}
