using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IRouteRepository : IGenericRepository<myRoute>
    {
        Task<myRoute> GetByIdAsync(int id);
        Task<myRoute> GetByRouteNameAsync(string routeName);
        Task<myRoute> AddAsync(myRoute route);
        Task<myRoute> UpdateAsync(myRoute route);
        Task DeleteAsync(int id);
        Task<bool> RouteExistsAsync(string routeName);
    }
}
