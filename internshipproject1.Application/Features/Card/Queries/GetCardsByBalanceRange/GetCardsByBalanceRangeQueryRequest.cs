using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardsByBalanceRange
{
    public class GetCardsByBalanceRangeQueryRequest : IRequest<IEnumerable<GetCardsByBalanceRangeQueryResponse>>
    {
        public decimal MinBalance { get; set; }
        public decimal MaxBalance { get; set; }
        public GetCardsByBalanceRangeQueryRequest(decimal minBalance, decimal maxBalance)
        {
            MinBalance = minBalance;
            MaxBalance = maxBalance;
        }
    }
}
