using internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByMenuId;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermission
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQueryRequest,GetPermissionQueryResponse>
    {
        private readonly IRoleMenuPermission _roleMenuPermission;

        public GetPermissionQueryHandler(IRoleMenuPermission roleMenuPermission)
        {
            _roleMenuPermission = roleMenuPermission;
        }

        public async Task<GetPermissionQueryResponse> Handle(GetPermissionQueryRequest request, CancellationToken cancellationToken)
        {
            var menu = await _roleMenuPermission.GetPermissionAsync(request.RoleId, request.MenuId, cancellationToken);
            return menu == null
                ? new GetPermissionQueryResponse
                {
                    RoleId = 0,
                    MenuId = request.MenuId,
                    CanRead = false,
                    CanCreate = false,
                    CanUpdate = false,
                    CanDelete = false
                }
                :
                new GetPermissionQueryResponse
                {
                    RoleId = menu.RoleId,
                    MenuId = menu.MenuId,
                    CanRead = menu.CanRead,
                    CanCreate = menu.CanCreate,
                    CanUpdate = menu.CanUpdate,
                    CanDelete = menu.CanDelete
                };
        }
    }
}
