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
using StackExchange.Redis;
using internshipproject1.Domain.Enums;
namespace internshipproject1.Application.Features.User.Commands.Register
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand,UserRegisterCommandResponse>
    {
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public UserRegisterHandler(IPasswordHashingService passwordHashingService, IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
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
                Role = UserRole.User,
                passwordHash = hash,
                passwordSalt = salt
            };
            await _userRepository.AddAsync(user, cancellationToken);
            var customer = new Domain.Entities.Customer
            {
                Name = command.Name,
                Surname = command.Surname,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                IsStudent = command.IsStudent,
                DateOfBirth = command.DateOfBirth,
                IsDeleted = false,
                UserId = user.Id
            };
            await _customerRepository.AddAsync(customer, cancellationToken);

            return new UserRegisterCommandResponse { 
            
                Id = user.Id,
                UserName = user.userName,
                Role = UserRole.User,
                Email = customer.Email,
                Name = customer.Name,
                Surname = customer.Surname,
                PhoneNumber = customer.PhoneNumber,
                IsStudent = customer.IsStudent,
                DateOfBirth = customer.DateOfBirth,
                IsDeleted = customer.IsDeleted,
                Message = $"Register Successfully ! Welcome {user.userName} !"
                
            };
        }

    }
}
