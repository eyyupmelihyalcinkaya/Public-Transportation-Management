using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetInactiveCards
{
    public class GetInactiveCardsQueryHandler : IRequestHandler<GetInactiveCardQueryRequest, List<GetInactiveCardQueryResponse>>
    {
        private readonly ICardRepository _cardRepository;
        public GetInactiveCardsQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<List<GetInactiveCardQueryResponse>> Handle(GetInactiveCardQueryRequest request, CancellationToken cancellationToken)
        { 
            var cards = await _cardRepository.GetAllInactiveCardsAsync(cancellationToken);
            if (cards == null || !cards.Any())
            {
                return new List<GetInactiveCardQueryResponse>();
            }
            return cards.Select(card => new GetInactiveCardQueryResponse
            {
                Id = card.Id,
                CustomerId = card.CustomerId,
                Balance = card.Balance,
                IsActive = card.IsActive,
                ExpirationDate = card.ExpirationDate
            }).ToList();
        }
    }
}
