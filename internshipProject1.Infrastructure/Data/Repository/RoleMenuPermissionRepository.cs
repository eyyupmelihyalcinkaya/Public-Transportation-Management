using internshipproject1.Application.Features.RoleMenuPermission.Queries.GetMenusWithPermissionsForUser;
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
    public class RoleMenuPermissionRepository : IRoleMenuPermission
    {
        private readonly AppDbContext _context;
        public RoleMenuPermissionRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<RoleMenuPermission> AddPermissionAsync(RoleMenuPermission permission, CancellationToken cancellationToken)
        {
            var roleMenuPermission = new RoleMenuPermission
            {
                RoleId = permission.RoleId,
                MenuId = permission.MenuId,
                CanCreate = permission.CanCreate,
                CanRead = permission.CanRead,
                CanUpdate = permission.CanUpdate,
                CanDelete = permission.CanDelete
            };
            await _context.RoleMenuPermission.AddAsync(roleMenuPermission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return roleMenuPermission;
        }

        public async Task<bool> DeletePermissionAsync(int roleId, int menuId, CancellationToken cancellationToken)
        {
            var rolePermission = await _context.RoleMenuPermission
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.MenuId == menuId, cancellationToken);
            if (rolePermission == null)
            {
                return false;
            }
            _context.RoleMenuPermission.Remove(rolePermission);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task<List<Menu>> GetAllowedMenusForRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            var menus = _context.RoleMenuPermission
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.Menu)
                .Include(m => m.SubMenus)
                .ToListAsync(cancellationToken);
            return menus;

        }

        public async Task<List<Menu>> GetMenusWithPermissionsForUserAsync(int userId, CancellationToken cancellationToken)
        {
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync(cancellationToken);

            return await _context.RoleMenuPermission
                .Where(rmp => userRoles.Contains(rmp.RoleId))
                .Include(rmp => rmp.Menu)
                .Select(rmp => rmp.Menu)
                .Distinct()
                .OrderBy(m => m.DisplayOrder)
                .ToListAsync(cancellationToken);
        }



        public async Task<RoleMenuPermission> GetPermissionAsync(int roleId, int menuId, CancellationToken cancellationToken)
        {
            var permission = await _context.RoleMenuPermission.FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.MenuId == menuId, cancellationToken);
            if (permission == null)
            {
                throw new KeyNotFoundException($"Permission not found for RoleId: {roleId} and MenuId: {menuId}");
            }
            return permission;
        }

        public async Task<List<RoleMenuPermission>> GetPermissionByMenuIdAsync(int menuId, CancellationToken cancellationToken)
        {
            var permission = await _context.RoleMenuPermission.Where(rp => rp.MenuId == menuId)
                .ToListAsync(cancellationToken);
            if (permission == null || !permission.Any())
            {
                throw new KeyNotFoundException($"No permissions found for MenuId: {menuId}");
            }
            return permission;
        }

        public Task<List<RoleMenuPermission>> GetPermissionByRoleIdAsync(int roleId, CancellationToken cancellationToken)
        {
            var permissions = _context.RoleMenuPermission
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync(cancellationToken);
            if (permissions == null)
            { 
                throw new KeyNotFoundException($"No permissions found for RoleId: {roleId}");
            }
            return permissions;
        }

        public async Task<bool> HasPermissionAsync(int userId, int menuId, string permissionType, CancellationToken cancellationToken)
        {
            var userRoles = await _context.UserRoles
                 .Where(ur => ur.UserId == userId)
                 .Select(ur => ur.RoleId)
                 .ToListAsync(cancellationToken);

            var query = _context.RoleMenuPermission
                .Where(rmp => userRoles.Contains(rmp.RoleId) && rmp.MenuId == menuId);

            return permissionType.ToLower() switch
            {
                "read" => await query.AnyAsync(rmp => rmp.CanRead, cancellationToken),
                "create" => await query.AnyAsync(rmp => rmp.CanCreate, cancellationToken),
                "update" => await query.AnyAsync(rmp => rmp.CanUpdate, cancellationToken),
                "delete" => await query.AnyAsync(rmp => rmp.CanDelete, cancellationToken),
                _ => false
            };
        }

        public Task<RoleMenuPermission> UpdatePermissionAsync(RoleMenuPermission permission, CancellationToken cancellationToken)
        {
            var existingPermission = _context.RoleMenuPermission
                .FirstOrDefault(rp => rp.RoleId == permission.RoleId && rp.MenuId == permission.MenuId);
            if (existingPermission == null)
            { 
                throw new KeyNotFoundException($"Permission not found for RoleId: {permission.RoleId} and MenuId: {permission.MenuId}");
            }
            existingPermission.CanCreate = permission.CanCreate;
            existingPermission.CanRead = permission.CanRead;
            existingPermission.CanUpdate = permission.CanUpdate;
            existingPermission.CanDelete = permission.CanDelete;
            _context.RoleMenuPermission.Update(existingPermission);
            _context.SaveChangesAsync(cancellationToken);
            return Task.FromResult(existingPermission);

        }
    }
}
