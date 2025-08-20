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
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public string PermissionType { get; set; }
        public HasPermissionQueryRequest(int userId, int menuId, string permissionType)
        {
            UserId = userId;
            MenuId = menuId;
            PermissionType = permissionType;
        }
    }
}
