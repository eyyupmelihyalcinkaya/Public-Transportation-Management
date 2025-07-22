using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsStudent { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; } 
    }
}
