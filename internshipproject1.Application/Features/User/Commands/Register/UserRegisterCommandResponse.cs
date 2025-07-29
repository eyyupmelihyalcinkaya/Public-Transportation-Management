using internshipproject1.Domain.Auth;
using internshipproject1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.User.Commands.Register
{
    public class UserRegisterCommandResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }
        public string Message { get; set; } = string.Empty;
        
    }
}
