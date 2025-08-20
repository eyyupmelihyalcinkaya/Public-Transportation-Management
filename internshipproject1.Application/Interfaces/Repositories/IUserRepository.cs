using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByIdAsync(int id,CancellationToken cancellationToken);
        Task<User> GetByIdWithCustomerAsync(int id, CancellationToken cancellationToken);
        Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> AddAsync(User user,CancellationToken cancellationToken);
        Task<User> UpdateAsync(User user,CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> UserExistsAsync(string username, CancellationToken cancellationToken);
        Task<User> ChangeRole(int id, CancellationToken cancellationToken);
    }
}
