using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.HasPermission
{
    public class HasPermissionQueryRequest : IRequest<HasPermissionQueryResponse>
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public string PermissionType { get; set; }
        public HasPermissionQueryRequest(int roleId, int menuId, string permissionType)
        {
            RoleId = roleId;
            MenuId = menuId;
            PermissionType = permissionType;
        }
    }
}
