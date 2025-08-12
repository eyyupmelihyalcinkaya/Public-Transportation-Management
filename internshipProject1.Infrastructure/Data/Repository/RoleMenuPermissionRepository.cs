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

        public async Task<List<Menu>> GetAllowedMenusForRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            var allowedMenus = await _context.RoleMenuPermission
                .Include(rp => rp.Menu)
                .Where(rp => rp.RoleId == roleId && rp.CanRead)
                .Select(rp => rp.Menu)
                .Distinct()
                .ToListAsync(cancellationToken);

            if (!allowedMenus.Any())
            {
                return new List<Menu>();
            }   
            var allowedMenuIds = allowedMenus.Select(m => m.Id).ToList();

            var hierarchicalMenus = await _context.Menu
                .Where(m => allowedMenuIds.Contains(m.Id))
                .Include(m => m.SubMenus.Where(sm => allowedMenuIds.Contains(sm.Id)))
                .Where(m => m.ParentMenuId == null)
                .OrderBy(m => m.DisplayOrder)
                .ToListAsync(cancellationToken);

            return hierarchicalMenus;
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
            var permissions = await _context.RoleMenuPermission
                .Include(rp => rp.Role) 
                .Include(rp => rp.Menu) 
                .Where(rp => rp.MenuId == menuId)
                .OrderBy(rp => rp.RoleId)
                .ToListAsync(cancellationToken);

            if (permissions == null || !permissions.Any())
            {
                return new List<RoleMenuPermission>();
            }

            return permissions;
        }
        public async Task<List<RoleMenuPermission>> GetPermissionByRoleIdAsync(int roleId, CancellationToken cancellationToken)
        {
            var permissions = await _context.RoleMenuPermission
                .Include(rp => rp.Role)
                .Include(rp => rp.Menu)
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync(cancellationToken);
                
            if (!permissions.Any())
            {
                return new List<RoleMenuPermission>();
            }
            return permissions;
        }

        public async Task<bool> HasPermissionAsync(int userId, int menuId, string permissionType, CancellationToken cancellationToken)
        {
            var userRoles = await _context.UserRoles
                 .Where(ur => ur.UserId == userId)
                 .Select(ur => ur.RoleId)
                 .ToListAsync(cancellationToken);

            if (userRoles == null || userRoles.Count == 0)
            {
                return false;
            }

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

        public async Task<RoleMenuPermission> UpdatePermissionAsync(RoleMenuPermission permission, CancellationToken cancellationToken)
        {
            var existingPermission = await _context.RoleMenuPermission
                .FirstOrDefaultAsync(rp => rp.RoleId == permission.RoleId && rp.MenuId == permission.MenuId, cancellationToken);
            if (existingPermission == null)
            { 
                throw new KeyNotFoundException($"Permission not found for RoleId: {permission.RoleId} and MenuId: {permission.MenuId}");
            }
            existingPermission.CanCreate = permission.CanCreate;
            existingPermission.CanRead = permission.CanRead;
            existingPermission.CanUpdate = permission.CanUpdate;
            existingPermission.CanDelete = permission.CanDelete;
            _context.RoleMenuPermission.Update(existingPermission);
            await _context.SaveChangesAsync(cancellationToken);
            return existingPermission;

        }
    }
}
