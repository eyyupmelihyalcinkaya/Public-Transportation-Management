using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using internshipProject1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipProject1.Infrastructure.Data.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;
        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AssignToRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
        {
            var userRole = new UserRoles
            {
                UserId = userId,
                RoleId = roleId
            };
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        public async Task<List<UserRoles>> GetAllAsync(CancellationToken cancellationToken)
        {
            var userRoles = await _context.UserRoles.ToListAsync(cancellationToken);
            if (userRoles == null || !userRoles.Any())
            {
                return new List<UserRoles>();
            }
            return userRoles;
        }

        public async Task<List<Role>> GetRolesByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            var userRoles = await _context.UserRoles.Where(u=>u.UserId == userId).Select(ur=>ur.Role).ToListAsync(cancellationToken);
            
            return userRoles;
        }

        public async Task<UserRoles> GetUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
        {
            var userRole = await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
            if (userRole == null)
            {
                throw new Exception($"User role not found for UserId: {userId} and RoleId: {roleId}");
            }
            return userRole;
        }

        public async Task<List<User>> GetUsersByRoleIdAsync(int roleId, CancellationToken cancellationToken)
        {
            var users = await _context.UserRoles.Where(ur=>ur.RoleId == roleId).Select(ur=>ur.User).ToListAsync(cancellationToken);
            if (users == null || !users.Any())
            {
                throw new Exception($"No users found for RoleId: {roleId}");
            }
            return users;
        }

        public async Task<bool> IsUserInRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles.AnyAsync(ur=>ur.UserId==userId && ur.RoleId == roleId, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception($"User role not found for UserId: {userId} and RoleId: {roleId}");
            }
        }
    }
}
