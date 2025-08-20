using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetBalanceByCardNumber
{
    public class GetBalanceByCardNumberQueryResponse
    {
        public decimal Balance { get; set; }
        public string CardNumber { get; set; }
    }
}
