using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Services;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Application.DTOs;
using internshipproject1.Domain.Entities;
using MediatR;
using internshipproject1.Application.Exceptions;
namespace internshipproject1.Application.Features.User.Commands.Register
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand,UserRegisterCommandResponse>
    {
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUserRepository _userRepository;
    
        public UserRegisterHandler(IPasswordHashingService passwordHashingService, IUserRepository userRepository)
        {
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
        }
        public async Task<UserRegisterCommandResponse> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.UserExistsAsync(command.userName, cancellationToken)) {
                throw new UserAlreadyRegisteredException("User Already Registered");
            }
            _passwordHashingService.CreatePasswordHash(command.password,out var hash, out var salt);
            var user = new internshipproject1.Domain.Entities.User
            {
                userName = command.userName,
                passwordHash = hash,
                passwordSalt = salt
            };
            await _userRepository.AddAsync(user, cancellationToken);
            return new UserRegisterCommandResponse { 
            
                Id = user.Id,
                UserName = user.userName,
                Message = $"Register Successfully ! Welcome {user.userName} !"
            };
        }

    }
}
