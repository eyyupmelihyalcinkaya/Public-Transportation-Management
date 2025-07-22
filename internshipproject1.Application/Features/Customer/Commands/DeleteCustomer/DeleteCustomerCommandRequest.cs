using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandRequest : IRequest<DeleteCustomerCommandResponse>
    {
        public int CustomerId { get; set; }
    
        public DeleteCustomerCommandRequest(int customerId)
        {
            CustomerId = customerId;
        }
        public DeleteCustomerCommandRequest()
        {
        }
    }
}
