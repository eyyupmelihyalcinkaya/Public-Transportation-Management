using MediatR;

namespace PaymentService.Features.Queries.GetByCardId
{
    public class GetByCardIdQueryRequest : IRequest<GetByCardIdQueryResponse>
    {
        public int CardId { get; set; }
        public GetByCardIdQueryRequest(int cardId)
        {
            CardId = cardId;
        }
    }
}
