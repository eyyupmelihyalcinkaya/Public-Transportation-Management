using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetAllCards
{
    public class GetAllCardsQueryHandler : IRequestHandler<GetAllCardsQueryRequest,  List<GetAllCardsQueryResponse>>
    {
        private readonly ICardRepository _cardRepository;

        public GetAllCardsQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<List<GetAllCardsQueryResponse>> Handle(GetAllCardsQueryRequest request, CancellationToken cancellationToken)
        { 
            var AllCards = await _cardRepository.GetAllAsync(cancellationToken);
            if (AllCards == null || !AllCards.Any())
            {
                return new List<GetAllCardsQueryResponse>();
            }
            return AllCards.Select(card => new GetAllCardsQueryResponse
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
