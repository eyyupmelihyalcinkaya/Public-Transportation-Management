using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using internshipproject1.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.User.Commands.ChangeRole
{
    public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommandRequest,ChangeRoleCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        public ChangeRoleCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ChangeRoleCommandResponse> Handle(ChangeRoleCommandRequest request, CancellationToken cancellationToken)
        {
            #region Eski Rolü Göstermek için yazdığım kod parçası
            var oldUser = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            var oldRole = oldUser.Role;
            var oldRoleString = "";
            if (oldUser.Role == UserRole.User) { oldRoleString = "User"; } else { oldRoleString = "Admin"; };
            #endregion

            var user = await _userRepository.ChangeRole(request.Id, cancellationToken); 
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User not found.");
            }
            
            var roleString = "";
            if (user.Role == UserRole.User) { roleString = "User"; } else { roleString = "Admin"; };
            return new ChangeRoleCommandResponse
            {
                Id = user.Id,
                UserName = user.userName,
                OldRole = oldRoleString,
                Role = roleString,
                Message = $"Role Changed Successfully ! {user.userName}'s New Role is : {roleString}"
            };
        }
    }
}
