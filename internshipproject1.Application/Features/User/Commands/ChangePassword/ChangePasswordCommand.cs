using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.User.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ChangePasswordCommandResponse>
    {
        public string UserName { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
