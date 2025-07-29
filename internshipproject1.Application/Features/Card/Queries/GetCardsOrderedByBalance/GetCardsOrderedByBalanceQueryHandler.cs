using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardsOrderedByBalance
{
    public class GetCardsOrderedByBalanceQueryHandler : IRequestHandler<GetCardsOrderedByBalanceQueryRequest, IEnumerable<GetCardsOrderedByBalanceQueryResponse>>
    {
        private readonly ICardRepository _cardRepository;

        public GetCardsOrderedByBalanceQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<IEnumerable<GetCardsOrderedByBalanceQueryResponse>> Handle(GetCardsOrderedByBalanceQueryRequest request, CancellationToken cancellationToken)
        {
            var cards = await _cardRepository.GetCardsOrderedByBalanceAsync(cancellationToken);
            var response = cards.Select(x => new GetCardsOrderedByBalanceQueryResponse
            {
                CardId = x.Id,
                Balance = x.Balance,
                ExpirationDate = x.ExpirationDate,
                IsActive = x.IsActive
            });
            return response;
        }
    }
}
