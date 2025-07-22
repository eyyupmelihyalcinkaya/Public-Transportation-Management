using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.DTOs
{
    public class CardDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
