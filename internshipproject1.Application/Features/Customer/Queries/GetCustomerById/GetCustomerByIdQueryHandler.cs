using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQueryRequest, GetCustomerByIdQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<GetCustomerByIdQueryResponse> Handle(GetCustomerByIdQueryRequest request, CancellationToken cancellationToken)
        { 
            var customer = await _customerRepository.GetByIdAsync(request.Id,cancellationToken);
            var response = new GetCustomerByIdQueryResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                IsStudent = customer.IsStudent,
                DateOfBirth = customer.DateOfBirth
            };
            return response;
        }
    }
}
