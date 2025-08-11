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
            var permissions = await _roleMenuPermission.GetPermissionByMenuIdAsync(request.MenuId, cancellationToken);

            if (permissions == null || !permissions.Any())
            {
                return new GetPermissionByMenuIdQueryResponse
                {
                    RoleId = 0,
                    MenuId = request.MenuId,
                    CanRead = false,
                    CanCreate = false,
                    CanUpdate = false,
                    CanDelete = false
                };
            }

            bool canRead = false, canCreate = false, canUpdate = false, canDelete = false;
            int roleId = 0;

            foreach (var p in permissions)
            {
                roleId = p.RoleId;
                canRead |= p.CanRead;
                canCreate |= p.CanCreate;
                canUpdate |= p.CanUpdate;
                canDelete |= p.CanDelete;
            }

            return new GetPermissionByMenuIdQueryResponse
            {
                RoleId = roleId,
                MenuId = request.MenuId,
                CanRead = canRead,
                CanCreate = canCreate,
                CanUpdate = canUpdate,
                CanDelete = canDelete
            };
        }
    }
}
