using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Enums;
using MediatR;
namespace internshipproject1.Application.Features.User.Commands.Register
{
    public class UserRegisterCommand : IRequest<UserRegisterCommandResponse>
    {
        public string userName { get; set; }
        public string password { get; set; }

    }
}
