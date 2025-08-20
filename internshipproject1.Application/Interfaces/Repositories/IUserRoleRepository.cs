using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task<List<UserRoles>> GetAllAsync(CancellationToken cancellationToken); //query
        Task AssignToRoleAsync(int userId, int roleId, CancellationToken cancellationToken); // command
        Task RemoveFromRoleAsync(int userId, int roleId, CancellationToken cancellationToken); // command
        Task<bool> IsUserInRoleAsync(int userId, int roleId, CancellationToken cancellationToken); // query
        Task<List<Role>> GetRolesByUserIdAsync(int userId, CancellationToken cancellationToken); // query
        Task<List<User>> GetUsersByRoleIdAsync(int roleId, CancellationToken cancellationToken); // query
        Task<UserRoles> GetUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken); // query
    }
}
