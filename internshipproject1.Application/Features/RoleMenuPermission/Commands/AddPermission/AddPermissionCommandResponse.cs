using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Commands.AddPermission
{
    public class AddPermissionCommandResponse
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public string Message { get; set; }
    }
}
