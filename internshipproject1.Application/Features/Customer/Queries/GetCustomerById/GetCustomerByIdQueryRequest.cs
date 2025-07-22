using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryRequest : IRequest<GetCustomerByIdQueryResponse>
    {
        public int Id { get; set; }
        public GetCustomerByIdQueryRequest(){}
        public GetCustomerByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
