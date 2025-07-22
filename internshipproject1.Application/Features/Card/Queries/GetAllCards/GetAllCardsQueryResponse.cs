using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetAllCards
{
    public class GetAllCardsQueryResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
