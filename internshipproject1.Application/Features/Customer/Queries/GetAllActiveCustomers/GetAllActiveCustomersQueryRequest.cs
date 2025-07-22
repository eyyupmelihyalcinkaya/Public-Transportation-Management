using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetAllActiveCustomers
{
    public class GetAllActiveCustomersQueryRequest : IRequest<IReadOnlyList<GetAllActiveCustomersQueryResponse>>
    {
    }
}
