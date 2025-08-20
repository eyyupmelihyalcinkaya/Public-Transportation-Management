using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Commands.DeletePermission
{
    public class DeletePermissionCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DeletePermissionCommandResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        
        public DeletePermissionCommandResponse()
        {
            IsSuccess = false;
            Message = "An error occurred while deleting the permission.";
        }
    }
}
