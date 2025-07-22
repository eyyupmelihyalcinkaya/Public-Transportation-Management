using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetAllActiveCustomers
{
    public class GetAllActiveCustomersQueryHandler : IRequestHandler<GetAllActiveCustomersQueryRequest,IReadOnlyList<GetAllActiveCustomersQueryResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllActiveCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IReadOnlyList<GetAllActiveCustomersQueryResponse>> Handle(GetAllActiveCustomersQueryRequest request, CancellationToken cancellationToken)
        { 
            var customers = await _customerRepository.GetAllActiveCustomersAsync(cancellationToken);
            if (customers == null || !customers.Any())
            {
                return new List<GetAllActiveCustomersQueryResponse>();
            }
            return customers.Select(c => new GetAllActiveCustomersQueryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                Email = c.Email,
                IsStudent = c.IsStudent,
                DateOfBirth = c.DateOfBirth,
                PhoneNumber = c.PhoneNumber
            }).ToList();
        }
    }
}
