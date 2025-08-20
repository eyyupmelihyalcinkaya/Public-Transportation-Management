using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Commands.DeletePermission
{
    public class DeletePermissionCommandRequest : IRequest<DeletePermissionCommandResponse>
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
    }
}
