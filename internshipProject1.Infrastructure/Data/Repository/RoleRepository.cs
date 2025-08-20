using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using internshipProject1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace internshipProject1.Infrastructure.Data.Repository

{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Role> AddAsync(Role role, CancellationToken cancellationToken)
        {
            var newRole = new Role
            {
                Name = role.Name,
                Description = role.Description
            };
            await _context.AddAsync(newRole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newRole;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var role = await _context.Role.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (role == null)
            {
                return false;
            }
            await _context.Role.Where(r => r.Id == id).ExecuteDeleteAsync(cancellationToken);
            if (role != null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Role>> GetAllAsync(CancellationToken cancellationToken)
        {
            var roles = await _context.Role.ToListAsync(cancellationToken);
            if (roles == null || !roles.Any())
            {
                throw new Exception("No roles found.");
            }
            return roles;
        }

        public async Task<Role> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var role = await _context.Role.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (role == null)
            {
                throw new Exception($"Role with ID {id} not found.");
            }
            return role;
        }

        public async Task<bool> IsExists(int id, CancellationToken cancellationToken)
        {
            var exists = await _context.Role.AnyAsync(r => r.Id == id, cancellationToken);
            if (!exists)
            {
                return false;
            }
            return true;
        }

        public async Task<Role> UpdateAsync(int id,Role role, CancellationToken cancellationToken)
        {
            var existingRole = _context.Role.FirstOrDefault(r => r.Id == id);
            if (existingRole == null)
            {
                throw new Exception($"Role with ID {role.Id} not found.");
            }
            existingRole.Name = role.Name;
            existingRole.Description = role.Description;
            await _context.SaveChangesAsync(cancellationToken);
            return existingRole;
        }
    }
}
