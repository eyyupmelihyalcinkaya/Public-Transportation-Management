using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest,DeleteRoleCommandResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        { 
            var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);

            var failed = new DeleteRoleCommandResponse
            {
                Id = request.Id,
                IsSuccess = false,
                Message = "Failed to delete role."
            };
            var success = new DeleteRoleCommandResponse
            {
                Id = request.Id,
                IsSuccess = true,
                Message = "Role deleted successfully."
            };
            if (role == null)
            {
                return new DeleteRoleCommandResponse
                {
                    Id = request.Id,
                    IsSuccess = false,
                    Message = "Role not found."
                };
            }
            var isDeleted = await _roleRepository.DeleteAsync(request.Id, cancellationToken);
            if (isDeleted)
            {
                return success;
            }
            else
            {
                return failed;
            }
        }
    }
}
