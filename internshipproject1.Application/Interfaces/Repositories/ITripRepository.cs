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
        public Task<Trip> GetByIdAsync(int id,CancellationToken cancellationToken);
        public Task<Trip> GetByRouteIdAsync(int routeId,CancellationToken cancellationToken);
        public Task<IReadOnlyList<Trip>> GetAllAsync(CancellationToken cancellationToken);
        public Task<Trip> AddAsync(Trip trip,CancellationToken cancellationToken);
        public Task<Trip> UpdateAsync(Trip trip,CancellationToken cancellationToken);
        public Task<Trip> DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<int> TotalTripsCount(CancellationToken cancellationToken);
    }
}
