using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Domain.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public Customer Customer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<CardTransaction> CardTransactions { get; set; }
        public bool IsDeleted { get; set; }

    }
}
