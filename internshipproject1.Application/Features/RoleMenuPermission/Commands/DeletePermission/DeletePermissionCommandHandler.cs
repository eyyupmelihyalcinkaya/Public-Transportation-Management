using internshipproject1.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.RoleMenuPermission.Commands.DeletePermission
{
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommandRequest, DeletePermissionCommandResponse>
    {
        private readonly IRoleMenuPermission _roleMenuPermissionRepository;
        public DeletePermissionCommandHandler(IRoleMenuPermission roleMenuPermissionRepository)
        {
            _roleMenuPermissionRepository = roleMenuPermissionRepository;
        }
        public async Task<DeletePermissionCommandResponse> Handle(DeletePermissionCommandRequest request, CancellationToken cancellationToken)
        {
            var isDeleted = await _roleMenuPermissionRepository.DeletePermissionAsync(request.RoleId, request.MenuId, cancellationToken);
            if (isDeleted)
            {
                return new DeletePermissionCommandResponse(true, "Permission deleted successfully.");
            }
            return new DeletePermissionCommandResponse(false, "Failed to delete permission. Permission not found.");
        }
    }



}
