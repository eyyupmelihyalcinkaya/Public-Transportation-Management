using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Commands.AssignToRole
{
    public class AssignToRoleCommandHandler : IRequestHandler<AssignToRoleCommandRequest, AssignToRoleCommandResponse>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public AssignToRoleCommandHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<AssignToRoleCommandResponse> Handle(AssignToRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var isUserInRole = await _userRoleRepository.IsUserInRoleAsync(request.UserId, request.RoleId, cancellationToken);
            if (isUserInRole)
            {
                return new AssignToRoleCommandResponse
                {
                    UserId = request.UserId,
                    RoleId = request.RoleId,
                    Message = "User is already assigned to this role.",
                    IsSuccess = false
                };
            }
            await _userRoleRepository.AssignToRoleAsync(request.UserId, request.RoleId, cancellationToken);
            return new AssignToRoleCommandResponse
            {
                UserId = request.UserId,
                RoleId = request.RoleId,
                Message = "User successfully assigned to role.",
                IsSuccess = true
            };
        }
    }
}
