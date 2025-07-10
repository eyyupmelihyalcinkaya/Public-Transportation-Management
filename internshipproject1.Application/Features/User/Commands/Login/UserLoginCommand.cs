using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.User.Commands.Login
{
    public class UserLoginCommand :IRequest<UserLoginCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
