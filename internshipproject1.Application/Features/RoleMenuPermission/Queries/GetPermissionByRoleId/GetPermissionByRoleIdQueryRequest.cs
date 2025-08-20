using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByRoleId
{
    public class GetPermissionByRoleIdQueryRequest : IRequest<List<GetPermissionByRoleIdQueryResponse>>
    {
        public int RoleId { get; set; }
        public GetPermissionByRoleIdQueryRequest(int roleId)
        {
            RoleId = roleId;
        }
    }
}
