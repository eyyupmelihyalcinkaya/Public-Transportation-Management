using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQueryRequest,IReadOnlyList<GetAllCustomersQueryResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IReadOnlyList<GetAllCustomersQueryResponse>> Handle(GetAllCustomersQueryRequest request, CancellationToken cancellationToken)
        { 
            var customers = await _customerRepository.GetAllAsync(cancellationToken);
            var response = customers.Select(c => new GetAllCustomersQueryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                Email = c.Email,
                IsActive = !c.IsDeleted,
                IsStudent = c.IsStudent
            }).ToList();
            return response;
        }
    }
}
