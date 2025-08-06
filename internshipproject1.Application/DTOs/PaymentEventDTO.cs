using internshipproject1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.DTOs
{
    public class PaymentEventDTO
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool isStudent { get; set; }
        public string? VehicleType { get; set; }
        public string Status { get; set; }
    }
}
