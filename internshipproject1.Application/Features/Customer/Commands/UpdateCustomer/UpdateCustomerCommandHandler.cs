using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        { 
            var customer = await _customerRepository.GetByIdAsync(request.Id,cancellationToken);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");
            }
            customer.Name = request.Name;
            customer.Surname = request.Surname;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            customer.IsStudent = request.IsStudent;
            customer.DateOfBirth = request.DateOfBirth;
            await _customerRepository.UpdateAsync(customer, cancellationToken);
            return new UpdateCustomerCommandResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                IsStudent = customer.IsStudent,
                DateOfBirth = customer.DateOfBirth,
                Message = "Customer updated successfully.",
                IsSuccess = true
            };
        }
    }
}
