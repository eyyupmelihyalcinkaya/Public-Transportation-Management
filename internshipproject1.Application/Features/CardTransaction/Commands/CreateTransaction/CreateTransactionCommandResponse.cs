using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.CreateTransaction
{
    public class CreateTransactionCommandResponse
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string? VehicleType { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
