using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetMenusWithPermissionsForUser
{
    public class GetMenusWithPermissionsForUserQueryRequest : IRequest<List<GetMenusWithPermissionsForUserQueryResponse>>
    {
        public int UserId { get; set; }
        public GetMenusWithPermissionsForUserQueryRequest(int userId)
        {
            UserId = userId;
        }
    }
}
