using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Commands.RemoveFromRole
{
    public class RemoveFromRoleCommandHandler : IRequestHandler<RemoveFromRoleCommandRequest, RemoveFromRoleCommandResponse>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public RemoveFromRoleCommandHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public async Task<RemoveFromRoleCommandResponse> Handle(RemoveFromRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var userInRole = await _userRoleRepository.IsUserInRoleAsync(request.UserId,request.RoleId, cancellationToken);
            if (!userInRole)
            {
                return new RemoveFromRoleCommandResponse
                {
                    IsSuccess = false,
                    Message = "User is not assigned to this role."
                };
            }
            await _userRoleRepository.RemoveFromRoleAsync(request.UserId, request.RoleId, cancellationToken);
            return new RemoveFromRoleCommandResponse
            {
                IsSuccess = true,
                Message = "User successfully removed from role."
            };
        }
    }

}
