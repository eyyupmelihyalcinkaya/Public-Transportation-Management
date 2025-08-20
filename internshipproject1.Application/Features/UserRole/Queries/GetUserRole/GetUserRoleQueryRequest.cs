using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetUserRole
{
    public class GetUserRoleQueryRequest : IRequest<GetUserRoleQueryResponse>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public GetUserRoleQueryRequest(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
