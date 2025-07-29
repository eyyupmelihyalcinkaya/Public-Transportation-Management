using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.User.Commands.ChangeRole
{
    public class ChangeRoleCommandRequest : IRequest<ChangeRoleCommandResponse>
    {
        public int Id { get; set; }
        public ChangeRoleCommandRequest(int id)
        {
            Id = id;
        }
    }
}
