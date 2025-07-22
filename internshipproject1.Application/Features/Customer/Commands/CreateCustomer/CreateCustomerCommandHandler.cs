using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = new Domain.Entities.Customer
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsStudent = request.IsStudent,
                DateOfBirth = request.DateOfBirth
            };
            await _customerRepository.AddAsync(customer,cancellationToken);
            return new CreateCustomerCommandResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Message = "Customer created successfully",
                IsSuccess = true
            };
        }
    }
}
