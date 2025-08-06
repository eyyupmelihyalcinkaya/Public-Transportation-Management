using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetCustomerByUserId
{
    public class GetCustomerByUserIdQueryRequest : IRequest<GetCustomerByUserIdQueryResponse>
    {
        public int UserId { get; set; }
        public GetCustomerByUserIdQueryRequest(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
            }
            UserId = userId;
        }
    }
}
