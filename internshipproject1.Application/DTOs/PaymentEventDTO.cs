using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.DTOs
{
    public class PaymentEventDTO
    {
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public string? VehicleType { get; set; }
        public string Status { get; set; }
    }
}
