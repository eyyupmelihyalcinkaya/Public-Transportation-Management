using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetCustomerByEmail
{
    public class GetCustomerByEmailQueryHandler : IRequestHandler<GetCustomerByEmailQueryRequest, GetCustomerByEmailQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerByEmailQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<GetCustomerByEmailQueryResponse> Handle(GetCustomerByEmailQueryRequest request, CancellationToken cancellationToken)
        { 
            var customerByEmail = await _customerRepository.GetByEmailAsync(request.Email,cancellationToken);
            if (customerByEmail == null)
            {
                throw new KeyNotFoundException($"Customer by {request.Email} not found");
            }
            return new GetCustomerByEmailQueryResponse
            {
                Id = customerByEmail.Id,
                Name = customerByEmail.Name,
                Surname = customerByEmail.Surname,
                Email = customerByEmail.Email,
                PhoneNumber = customerByEmail.PhoneNumber
            };

        }
    }
}
