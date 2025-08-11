using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByMenuId
{
    public class GetPermissionByMenuIdQueryRequest : IRequest<GetPermissionByMenuIdQueryResponse>
    {
        public int MenuId { get; set; }
        public GetPermissionByMenuIdQueryRequest(int menuId)
        {
            MenuId = menuId;
        }
    }
}
