using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetCustomerByNameAndSurname
{
    public class GetCustomerByNameAndSurnameQueryHandler: IRequestHandler<GetCustomerByNameAndSurnameQueryRequest, IReadOnlyList<GetCustomerByNameAndSurnameQueryResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerByNameAndSurnameQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IReadOnlyList<GetCustomerByNameAndSurnameQueryResponse>> Handle(GetCustomerByNameAndSurnameQueryRequest request, CancellationToken cancellationToken)
        { 
            var customer = await _customerRepository.GetAllByNameAndSurnameAsync(request.Name, request.Surname, cancellationToken);
            var customers = customer.Select(c => new GetCustomerByNameAndSurnameQueryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                IsActive = !c.IsDeleted,
                IsStudent = c.IsStudent,
                DateOfBirth = c.DateOfBirth
            }).ToList();
            return customers;
        }
    }
}
