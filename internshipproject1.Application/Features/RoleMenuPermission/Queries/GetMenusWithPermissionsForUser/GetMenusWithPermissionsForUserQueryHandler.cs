using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetMenusWithPermissionsForUser
{
    public class GetMenusWithPermissionsForUserQueryHandler : IRequestHandler<GetMenusWithPermissionsForUserQueryRequest, List<GetMenusWithPermissionsForUserQueryResponse>>
    {
        private readonly IRoleMenuPermission _roleMenuPermission;

        public GetMenusWithPermissionsForUserQueryHandler(IRoleMenuPermission roleMenuPermission)
        {
            _roleMenuPermission = roleMenuPermission;
        }
        public async Task<List<GetMenusWithPermissionsForUserQueryResponse>> Handle(GetMenusWithPermissionsForUserQueryRequest request,CancellationToken cancellationToken)
        {
            var menus = await _roleMenuPermission.GetMenusWithPermissionsForUserAsync(request.UserId, cancellationToken);

            var result = new List<GetMenusWithPermissionsForUserQueryResponse>();

            foreach (var menu in menus)
            {
                var permission = await _roleMenuPermission.GetPermissionAsync(request.UserId, menu.Id, cancellationToken);

                result.Add(new GetMenusWithPermissionsForUserQueryResponse
                {
                    RoleId = permission.RoleId,
                    MenuId = menu.Id,
                    CanRead = permission.CanRead,
                    CanCreate = permission.CanCreate,
                    CanUpdate = permission.CanUpdate,
                    CanDelete = permission.CanDelete
                });
            }

            return result;
        }

    }
}
