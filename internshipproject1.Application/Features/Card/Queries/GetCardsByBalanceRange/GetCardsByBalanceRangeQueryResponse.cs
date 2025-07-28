using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardsByBalanceRange
{
    public class GetCardsByBalanceRangeQueryResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        

    }
}
