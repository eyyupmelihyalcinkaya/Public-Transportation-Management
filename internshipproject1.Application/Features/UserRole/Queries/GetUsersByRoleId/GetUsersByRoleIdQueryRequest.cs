using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetUsersByRoleId
{
    public class GetUsersByRoleIdQueryRequest : IRequest<List<GetUsersByRoleIdQueryResponse>>
    {
        public int RoleId { get; set; }
        public GetUsersByRoleIdQueryRequest(int roleId)
        {
            RoleId = roleId;
        }
    }
}
