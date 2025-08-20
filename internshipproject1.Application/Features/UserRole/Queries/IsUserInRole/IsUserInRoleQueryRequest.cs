using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.IsUserInRole
{
    public class IsUserInRoleQueryRequest : IRequest<IsUserInRoleQueryResponse>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
