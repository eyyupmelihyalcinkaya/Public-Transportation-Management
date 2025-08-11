using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Commands.UpdatePermission
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommandRequest, UpdatePermissionCommandResponse>
    {
        private readonly IRoleMenuPermission _roleMenuPermission;
        public UpdatePermissionCommandHandler(IRoleMenuPermission roleMenuPermission)
        {
            _roleMenuPermission = roleMenuPermission;
        }
        public async Task<UpdatePermissionCommandResponse> Handle(UpdatePermissionCommandRequest request, CancellationToken cancellationToken)
        {
            var permission = await _roleMenuPermission.GetPermissionAsync(request.RoleId, request.MenuId, cancellationToken);
            if (permission == null)
            {
                return new UpdatePermissionCommandResponse
                {
                    Message = "Permission not found.",
                    IsSuccess = false
                };
            }
            permission.CanRead = request.CanRead;
            permission.CanCreate = request.CanCreate;
            permission.CanUpdate = request.CanUpdate;
            permission.CanDelete = request.CanDelete;
            var updatedPermission = await _roleMenuPermission.UpdatePermissionAsync(permission, cancellationToken);
            return new UpdatePermissionCommandResponse
            {
                Message = updatedPermission != null
                  ? "Permission updated successfully."
                  : "Permission update failed.",
                IsSuccess = updatedPermission != null
            };
        }
    }
}
