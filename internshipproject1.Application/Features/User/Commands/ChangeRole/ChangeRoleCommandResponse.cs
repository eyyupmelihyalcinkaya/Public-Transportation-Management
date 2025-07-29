using internshipproject1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.User.Commands.ChangeRole
{
    public class ChangeRoleCommandResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OldRole { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
    }
}
