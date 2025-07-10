using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;
namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IStopRepository : IGenericRepository<Stop>
    {

        Task<Stop> GetByIdAsync(int id,CancellationToken cancellationToken);
        Task<Stop> GetByStopNameAsync(string stopName,CancellationToken cancellationToken);
        Task<Stop> AddAsync(Stop stop,CancellationToken cancellationToken);
        Task<Stop> UpdateAsync(Stop stop,CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> StopExistsAsync(string stopName, CancellationToken cancellationToken);
        Task<IReadOnlyList<Stop>> GetAllAsync(CancellationToken cancellationToken);

    }
}
