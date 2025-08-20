using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetRolesByUserId
{
    public class GetRolesByUserIdQueryResponse
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }

    }
}
