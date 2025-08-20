using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Commands.RemoveFromRole
{
    public class RemoveFromRoleCommandRequest : IRequest<RemoveFromRoleCommandResponse>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
