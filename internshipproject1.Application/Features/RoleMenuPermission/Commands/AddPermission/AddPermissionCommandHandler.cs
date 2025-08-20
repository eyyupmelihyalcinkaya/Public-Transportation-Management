using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Commands.AddPermission
{
    public class AddPermissionCommandHandler : IRequestHandler<AddPermissionCommandRequest, AddPermissionCommandResponse>
    {
        private readonly IRoleMenuPermission _roleMenuPermission;

        public AddPermissionCommandHandler(IRoleMenuPermission roleMenuPermission)
        {
            _roleMenuPermission = roleMenuPermission;
        }

        public async Task<AddPermissionCommandResponse> Handle(AddPermissionCommandRequest request, CancellationToken cancellationToken)
        { 
            var permission = new Domain.Entities.RoleMenuPermission
            {
                RoleId = request.RoleId,
                MenuId = request.MenuId,
                CanRead = request.CanRead,
                CanCreate = request.CanCreate,
                CanUpdate = request.CanUpdate,
                CanDelete = request.CanDelete
            };
            var addedPermission = await _roleMenuPermission.AddPermissionAsync(permission, cancellationToken);
            if (addedPermission == null)
            {
                throw new ApplicationException("Permission could not be added.");
            }
            return new AddPermissionCommandResponse
            {
                RoleId = addedPermission.RoleId,
                MenuId = addedPermission.MenuId,
                CanRead = addedPermission.CanRead,
                CanCreate = addedPermission.CanCreate,
                CanUpdate = addedPermission.CanUpdate,
                CanDelete = addedPermission.CanDelete,
                Message = "Permission added successfully."
            };
        }
    }
}
