using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandResponse
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

    }
}
