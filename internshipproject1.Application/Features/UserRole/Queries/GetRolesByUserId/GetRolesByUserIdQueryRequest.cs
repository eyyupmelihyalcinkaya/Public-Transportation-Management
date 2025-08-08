using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetRolesByUserId
{
    public class GetRolesByUserIdQueryRequest : IRequest<GetRolesByUserIdQueryResponse>
    {
        public int UserId { get; set; }
        public GetRolesByUserIdQueryRequest(int userId)
        {
            UserId = userId;
        }
    }
}
