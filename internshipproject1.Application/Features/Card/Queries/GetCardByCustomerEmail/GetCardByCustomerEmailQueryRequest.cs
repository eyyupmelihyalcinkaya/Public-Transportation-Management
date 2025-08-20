using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardByCustomerEmail
{
    public class GetCardByCustomerEmailQueryRequest : IRequest<GetCardByCustomerEmailQueryResponse>
    {
        public string Email { get; set; }
        public GetCardByCustomerEmailQueryRequest(string email)
        {
            Email = email;
        }
    }
}
