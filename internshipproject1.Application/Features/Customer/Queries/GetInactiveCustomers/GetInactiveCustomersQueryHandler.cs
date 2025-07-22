using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetInactiveCustomers
{
    public class GetInactiveCustomersQueryHandler : IRequestHandler<GetInactiveCustomersQueryRequest, IReadOnlyList<GetInactiveCustomersQueryResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetInactiveCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IReadOnlyList<GetInactiveCustomersQueryResponse>> Handle(GetInactiveCustomersQueryRequest request, CancellationToken cancellationToken)
        { 
            var customers = await _customerRepository.GetAllInactiveCustomersAsync(cancellationToken);
            var response = customers.Select(c => new GetInactiveCustomersQueryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                IsStudent = c.IsStudent,
                DateOfBirth = c.DateOfBirth,
                IsDeleted = c.IsDeleted
            }).ToList();
            return response;
        }
    }
}
