using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IRoleMenuPermission
    {

        Task<List<RoleMenuPermission>> GetPermissionByRoleIdAsync(int roleId,CancellationToken cancellationToken); // query
        Task<List<RoleMenuPermission>> GetPermissionByMenuIdAsync(int menuId, CancellationToken cancellationToken); // query
        Task<RoleMenuPermission> GetPermissionAsync(int roleId, int menuId, CancellationToken cancellationToken); // query
        Task<List<Menu>> GetAllowedMenusForRoleAsync(int roleId, CancellationToken cancellationToken); // query
        Task<List<Menu>> GetMenusWithPermissionsForUserAsync(int userId, CancellationToken cancellationToken); // query
        Task<bool> HasPermissionAsync(int userId, int menuId, string permissionType, CancellationToken cancellationToken); // query
        Task<RoleMenuPermission> AddPermissionAsync(RoleMenuPermission permission, CancellationToken cancellationToken); // command
        Task<RoleMenuPermission> UpdatePermissionAsync(RoleMenuPermission permission, CancellationToken cancellationToken); // command
        Task<bool> DeletePermissionAsync(int roleId, int menuId, CancellationToken cancellationToken); // command




    }
}
