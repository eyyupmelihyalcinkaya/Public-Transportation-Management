using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        { 
            var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
            {
                return new UpdateRoleCommandResponse
                {
                    IsSuccess = false,
                    Message = "Role not found"
                };
            }
            role.Name = request.Name;
            role.Description = request.Description;
            var updatedRole = await _roleRepository.UpdateAsync(request.Id, role, cancellationToken);
            if (updatedRole == null)
            {
                return new UpdateRoleCommandResponse
                {
                    IsSuccess = false,
                    Message = "Failed to update role"
                };
            }
            return new UpdateRoleCommandResponse
            {
                Id = updatedRole.Id,
                Name = updatedRole.Name,
                Description = updatedRole.Description,
                IsSuccess = true,
                Message = "Role updated successfully"
            };
        }
    }
}
