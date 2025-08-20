using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;
namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(int id, CancellationToken cancellationToken); // query
        Task<List<Role>> GetAllAsync(CancellationToken cancellationToken); // query
        Task<Role> AddAsync(Role role, CancellationToken cancellationToken); // command
        Task<Role> UpdateAsync(int id,Role role, CancellationToken cancellationToken); // command
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken); // command
        Task<bool> IsExists(int id, CancellationToken cancellationToken); // query
    }
}
