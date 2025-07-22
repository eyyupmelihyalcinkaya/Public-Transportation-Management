using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommandRequest, DeleteCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<DeleteCustomerCommandResponse> Handle(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
        { 
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId,cancellationToken);
            if (customer == null)
            {
                return new DeleteCustomerCommandResponse
                {
                    IsSuccess = false,
                    Message = "Customer not found."
                };
            }
            customer.IsDeleted = true;
            await _customerRepository.UpdateAsync(customer, cancellationToken);
            return new DeleteCustomerCommandResponse
            {
                CustomerId = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                IsSuccess = true,
                Message = "Customer deleted successfully."
            };
        }
    }
}
