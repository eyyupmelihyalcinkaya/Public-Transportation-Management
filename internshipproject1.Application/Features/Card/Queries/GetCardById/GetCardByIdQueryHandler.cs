using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardById
{
    public class GetCardByIdQueryHandler: IRequestHandler<GetCardByIdQueryRequest, GetCardByIdQueryResponse>
    {
        private readonly ICardRepository _cardRepository;

        public GetCardByIdQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<GetCardByIdQueryResponse> Handle(GetCardByIdQueryRequest request, CancellationToken cancellationToken)
        { 
            var card = await _cardRepository.GetByIdAsync(request.Id, cancellationToken);
            if(card == null)
            {
                throw new Exception($"Card with ID {request.Id} not found.");
            }
            return new GetCardByIdQueryResponse
            {
                Id = card.Id,
                CustomerId = card.CustomerId,
                Balance = card.Balance,
                IsActive = card.IsActive,
                ExpirationDate = card.ExpirationDate
            };
        }
    }
}
