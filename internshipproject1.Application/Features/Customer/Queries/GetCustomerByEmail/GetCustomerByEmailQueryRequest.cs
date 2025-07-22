using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetCustomerByEmail
{
    public class GetCustomerByEmailQueryRequest : IRequest<GetCustomerByEmailQueryResponse>
    {
        public string Email { get; set; }
        public GetCustomerByEmailQueryRequest(string email)
        {
            Email = email;
        }
    }
}
