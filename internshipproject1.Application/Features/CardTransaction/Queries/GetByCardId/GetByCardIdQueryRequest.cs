using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Queries.GetByCardId
{
    public class GetByCardIdQueryRequest : IRequest<GetByCardIdQueryResponse>
    {
        public int CardId { get; set; }
        public GetByCardIdQueryRequest(int cardId)
        {
            CardId = cardId;
        }
        public GetByCardIdQueryRequest()
        {
        }
    }
}
