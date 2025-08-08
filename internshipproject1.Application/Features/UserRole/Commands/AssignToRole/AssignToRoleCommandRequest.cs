using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Commands.AssignToRole
{
    public class AssignToRoleCommandRequest : IRequest<AssignToRoleCommandResponse>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public AssignToRoleCommandRequest(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
