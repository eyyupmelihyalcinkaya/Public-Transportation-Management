using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;

namespace internshipproject1.Application.Features.Card.Queries.GetAllActiveCards
{
    public class GetAllActiveCardsQueryHandler : IRequestHandler<GetAllActiveCardsQueryRequest,List<GetAllActiveCardsQueryResponse>>
    {
        private readonly ICardRepository _cardRepository;
        
        public GetAllActiveCardsQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<List<GetAllActiveCardsQueryResponse>> Handle(GetAllActiveCardsQueryRequest request, CancellationToken cancellationToken)
        { 
            var cards = await _cardRepository.GetAllActiveCardsAsync(cancellationToken);
            if(cards == null || !cards.Any())
            {
                return new List<GetAllActiveCardsQueryResponse>();
            }
            return cards.Select(card => new GetAllActiveCardsQueryResponse
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
