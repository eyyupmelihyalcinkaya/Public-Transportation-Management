using internshipproject1.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Application.DTOs;
using System.Threading.Tasks;
using internshipproject1.Domain.Auth;
using Microsoft.Extensions.Configuration;
using MediatR;
using internshipproject1.Application.Exceptions;

namespace internshipproject1.Application.Features.User.Commands.Login
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, UserLoginCommandResponse>
    {
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserLoginHandler(IPasswordHashingService passwordHashingService, IUserRepository userRepository, IConfiguration configuration, ICustomerRepository customerRepository,IUserRoleRepository userRoleRepository)
        {
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
            _configuration = configuration;
            _customerRepository = customerRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<UserLoginCommandResponse> Handle(UserLoginCommand command,CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByUsernameAsync(command.UserName,cancellationToken);
            
            if (user == null)
            {
                throw new WrongUsernameOrPasswordException("Kullanıcı adı veya şifre hatalı");
            }

            var customer = await _customerRepository.GetByUserIdAsync(user.Id,cancellationToken);
            if (customer == null)
            {
                throw new Exception("Customer's cannot found from UserId ---USER LOGIN HANDLER---");
            }
            var mail = customer.Email;
            bool isPasswordValid = _passwordHashingService.VerifyPasswordHash(
                command.Password, 
                user.passwordHash, 
                user.passwordSalt);

            if (!isPasswordValid)
            {
                throw new WrongUsernameOrPasswordException("Kullanıcı adı veya şifre hatalı");
            }

            // Kullanıcının rollerini çek ve yoksa default Passenger ata
            var roles = await _userRoleRepository.GetRolesByUserIdAsync(user.Id, cancellationToken);
            if (roles == null || !roles.Any())
            {
                await _userRoleRepository.AssignToRoleAsync(user.Id, 3, cancellationToken);
                roles = await _userRoleRepository.GetRolesByUserIdAsync(user.Id, cancellationToken);
            }

            var roleNames = roles.Select(r => r.Name).ToList();
            var primaryRoleId = roles.FirstOrDefault()?.Id ?? 3;

            var token = TokenHandler.CreateToken(
                _configuration,
                user.userName,
                user.Id,
                roleNames,
                new Dictionary<string, string> { { "email", mail } }
            );

            return new UserLoginCommandResponse
            {
                Id = user.Id,
                UserName = user.userName,
                Role = primaryRoleId,
                Message = $"Login Successfully, Welcome Back {user.userName} !",
                Token = token,
                Email = mail
            };
        }
    }
}
