using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Services;
using MediatR;
using internshipproject1.Domain.Exceptions;
namespace internshipproject1.Application.Features.User.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand,ChangePasswordCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        public ChangePasswordHandler(IUserRepository userRepository, IPasswordHashingService passwordHashingService) { 
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) {
            var user = await _userRepository.GetByUsernameAsync(request.UserName);
            if (user == null) {
                throw new KeyNotFoundException($"User '{request.UserName}' does not exist.");
            }
            bool oldPasswordValification = _passwordHashingService.VerifyPasswordHash(request.oldPassword, user.passwordHash, user.passwordSalt);
            if (!oldPasswordValification) {
                throw new InvalidPasswordException("Wrong Password !");
            }
            _passwordHashingService.CreatePasswordHash(request.newPassword, out var hash, out var salt);
            user.passwordHash = hash;
            user.passwordSalt = salt;
            await _userRepository.UpdateAsync(user);
            return new ChangePasswordCommandResponse
            {
                UserName = user.userName,
                Message = "Password Changed Successfully"
            };
        }
    }
}
