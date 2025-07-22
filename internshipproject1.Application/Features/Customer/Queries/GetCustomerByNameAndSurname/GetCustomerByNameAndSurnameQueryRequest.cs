using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetCustomerByNameAndSurname
{
    public class GetCustomerByNameAndSurnameQueryRequest : IRequest<IReadOnlyList<GetCustomerByNameAndSurnameQueryResponse>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public GetCustomerByNameAndSurnameQueryRequest(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public GetCustomerByNameAndSurnameQueryRequest()
        {
        }
    }
}
