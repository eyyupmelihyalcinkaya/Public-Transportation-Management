using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.User.Commands.ChangePassword
{
    public class ChangePasswordCommandResponse
    {
        public string UserName { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
