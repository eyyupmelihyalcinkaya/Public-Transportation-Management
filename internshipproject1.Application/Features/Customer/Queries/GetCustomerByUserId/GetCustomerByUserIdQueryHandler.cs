using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetCustomerByUserId
{
    public class GetCustomerByUserIdQueryHandler : IRequestHandler<GetCustomerByUserIdQueryRequest,GetCustomerByUserIdQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerByUserIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<GetCustomerByUserIdQueryResponse> Handle(GetCustomerByUserIdQueryRequest request, CancellationToken cancellationToken)
        { 
            var customerExists = await _customerRepository.CustomerExistsByUserId(request.UserId, cancellationToken);
            if (!customerExists)
            {
                throw new KeyNotFoundException($"Customer with User ID {request.UserId} not found.");
            }
            var customer = await _customerRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with User ID {request.UserId} not found or has been deleted.");
            }
            return new GetCustomerByUserIdQueryResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                IsStudent = customer.IsStudent,
                DateOfBirth = customer.DateOfBirth,
                IsDeleted = customer.IsDeleted,
                UserId = customer.UserId
            };
        }
    }
}
