using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardsOrderedByBalance
{
    public class GetCardsOrderedByBalanceQueryResponse
    {
        public int CardId { get; set; }
        public decimal Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        
    }
}
