using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByRoleId
{
    public class GetPermissionByRoleIdQueryHandler : IRequestHandler<GetPermissionByRoleIdQueryRequest,List<GetPermissionByRoleIdQueryResponse>>
    {
        private readonly IRoleMenuPermission _roleMenuPermission;

        public GetPermissionByRoleIdQueryHandler(IRoleMenuPermission roleMenuPermission)
        {
            _roleMenuPermission = roleMenuPermission;
        }

        public async Task<List<GetPermissionByRoleIdQueryResponse>> Handle(GetPermissionByRoleIdQueryRequest request, CancellationToken cancellationToken)
        { 
            var permissions = await _roleMenuPermission.GetPermissionByRoleIdAsync(request.RoleId, cancellationToken);
            if(permissions == null || !permissions.Any())
            {
                return new List<GetPermissionByRoleIdQueryResponse>
                {
                    new GetPermissionByRoleIdQueryResponse
                    {
                        RoleId = request.RoleId,
                        MenuId = 0,
                        CanRead = false,
                        CanCreate = false,
                        CanUpdate = false,
                        CanDelete = false
                    }
                };
            }
            return permissions.Select(p => new GetPermissionByRoleIdQueryResponse
            {
                RoleId = p.RoleId,
                MenuId = p.MenuId,
                CanRead = p.CanRead,
                CanCreate = p.CanCreate,
                CanUpdate = p.CanUpdate,
                CanDelete = p.CanDelete,
                MenuName = p.Menu?.Name ?? string.Empty,
                MenuUrl = p.Menu?.Url ?? string.Empty,
                RoleName = p.Role?.Name ?? string.Empty
            }).ToList();
        }
    }
}
