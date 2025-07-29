using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardsOrderedByBalance
{
    public class GetCardsOrderedByBalanceQueryRequest : IRequest<IEnumerable<GetCardsOrderedByBalanceQueryResponse>>
    {
    }
}
