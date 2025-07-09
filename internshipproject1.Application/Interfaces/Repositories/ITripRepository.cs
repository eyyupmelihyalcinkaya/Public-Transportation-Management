using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface ITripRepository : IGenericRepository<Trip>
    {
        public Task<Trip> GetByIdAsync(int id);
        public Task<Trip> GetByRouteIdAsync(int routeId);
        public Task<IReadOnlyList<Trip>> GetAllAsync();
        public Task<Trip> AddAsync(Trip trip);
        public Task<Trip> UpdateAsync(Trip trip);
        public Task<Trip> DeleteAsync(int id);
    }
}
