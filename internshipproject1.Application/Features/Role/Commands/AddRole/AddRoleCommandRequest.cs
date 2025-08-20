using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Role.Commands.AddRole
{
    public class AddRoleCommandRequest : IRequest<AddRoleCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AddRoleCommandRequest()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }

}
