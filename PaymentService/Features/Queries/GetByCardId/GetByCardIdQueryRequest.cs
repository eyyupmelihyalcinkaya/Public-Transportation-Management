using MediatR;

namespace PaymentService.Features.Queries.GetByCardId
{
    public class GetByCardIdQueryRequest : IRequest<GetByCardIdQueryResponse>
    {
        public string CardNumber { get; set; }
        public GetByCardIdQueryRequest(string CardNumber)
        {
            CardNumber = CardNumber;
        }
    }
}
