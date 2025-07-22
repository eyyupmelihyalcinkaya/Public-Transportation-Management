using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Features.Card.Queries.GetAllActiveCards;
namespace internshipproject1.Application.Features.Card.Queries.GetAllActiveCards
{
    public class GetAllActiveCardsQueryResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
