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

        Task<Stop> GetByIdAsync(int id);
        Task<Stop> GetByStopNameAsync(string stopName);
        Task<Stop> AddAsync(Stop stop);
        Task<Stop> UpdateAsync(Stop stop);
        Task DeleteAsync(int id);
        Task<bool> StopExistsAsync(string stopName);
        Task<IReadOnlyList<Stop>> GetAllAsync();

    }
}
