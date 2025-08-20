using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Role.Commands.AddRole
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommandRequest,AddRoleCommandResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public AddRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<AddRoleCommandResponse> Handle(AddRoleCommandRequest request, CancellationToken cancellationToken)
        { 
            var role = new Domain.Entities.Role
            {
                Name = request.Name,
                Description = request.Description
            };
            var addedRole = await _roleRepository.AddAsync(role, cancellationToken);
            return new AddRoleCommandResponse
            {
                Id = addedRole.Id,
                Name = addedRole.Name,
                Description = addedRole.Description,
                Message = "Role added successfully",
                IsSuccess = true
            };
        }
    }
}
