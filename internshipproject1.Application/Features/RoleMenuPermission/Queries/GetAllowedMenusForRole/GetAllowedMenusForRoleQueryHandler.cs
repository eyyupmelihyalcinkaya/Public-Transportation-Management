using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetAllowedMenusForRole
{
    public class GetAllowedMenusForRoleQueryHandler : IRequestHandler<GetAllowedMenusForRoleQueryRequest, GetAllowedMenusForRoleQueryResponse>
    {
        private readonly IRoleMenuPermission _roleMenuPermission;

        public GetAllowedMenusForRoleQueryHandler(IRoleMenuPermission roleMenuPermission)
        {
            _roleMenuPermission = roleMenuPermission;
        }

        public async Task<GetAllowedMenusForRoleQueryResponse> Handle(GetAllowedMenusForRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var menus = await _roleMenuPermission.HasPermissionAsync(request.RoleId,request.MenuId,request.PermissionType, cancellationToken);
            return new GetAllowedMenusForRoleQueryResponse
            {
                HasPermission = menus
            };
        }
    }
}
