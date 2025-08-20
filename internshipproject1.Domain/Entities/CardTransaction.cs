using internshipproject1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Domain.Entities
{
    public class CardTransaction
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string? VehicleType { get; set; }
        public string? Description { get; set; }
        public Card Card { get; set; }
        public bool IsDeleted { get; set; }

    }
}
