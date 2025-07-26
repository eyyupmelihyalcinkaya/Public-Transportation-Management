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
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string? VehicleType { get; set; }
        public string? Description { get; set; }
        public Card Card { get; set; }
        public bool IsDeleted { get; set; }

    }
}
