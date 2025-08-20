using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermission
{
    public class GetPermissionQueryRequest : IRequest<GetPermissionQueryResponse>
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public GetPermissionQueryRequest(int roleId, int menuId)
        {
            RoleId = roleId;
            MenuId = menuId;
        }
    }
}
