using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByMenuId
{
    public class GetPermissionByMenuIdQueryHandler : IRequestHandler<GetPermissionByMenuIdQueryRequest,GetPermissionByMenuIdQueryResponse>
    {
        private readonly IRoleMenuPermission _roleMenuPermission;

        public GetPermissionByMenuIdQueryHandler(IRoleMenuPermission roleMenuPermission)
        {
            _roleMenuPermission = roleMenuPermission;
        }

        public async Task<GetPermissionByMenuIdQueryResponse> Handle(GetPermissionByMenuIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var permissions = await _roleMenuPermission.GetPermissionByMenuIdAsync(request.MenuId, cancellationToken);

                if (permissions == null || !permissions.Any())
                {
                    return new GetPermissionByMenuIdQueryResponse
                    {
                        MenuId = request.MenuId,
                        RolePermissions = new List<RolePermissionDTO>(),
                        Message = "No permissions found for this menu",
                        HasAnyPermission = false
                    };
                }

                
                var rolePermissions = permissions.Select(p => new RolePermissionDTO
                {
                    RoleId = p.RoleId,
                    RoleName = GetRoleName(p.RoleId),
                    CanRead = p.CanRead,
                    CanCreate = p.CanCreate,
                    CanUpdate = p.CanUpdate,
                    CanDelete = p.CanDelete
                }).ToList();

                return new GetPermissionByMenuIdQueryResponse
                {
                    MenuId = request.MenuId,
                    RolePermissions = rolePermissions,
                    Message = $"Found {rolePermissions.Count} role permissions for menu",
                    HasAnyPermission = rolePermissions.Any(rp => rp.HasAnyPermission)
                };
            }
            catch (Exception ex)
            {
                return new GetPermissionByMenuIdQueryResponse
                {
                    MenuId = request.MenuId,
                    RolePermissions = new List<RolePermissionDTO>(),
                    Message = $"Error retrieving permissions: {ex.Message}",
                    HasAnyPermission = false
                };
            }
        }
        private string GetRoleName(int roleId)
        {
            return roleId switch
            {
                1 => "SuperAdmin",
                2 => "Admin",
                3 => "Passenger",
                _ => "Unknown"
            };
        }
    }
}
