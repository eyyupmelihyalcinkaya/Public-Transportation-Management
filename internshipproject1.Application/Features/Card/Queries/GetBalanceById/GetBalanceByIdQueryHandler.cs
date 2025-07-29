using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetBalanceById
{
    public class GetBalanceByIdQueryHandler : IRequestHandler<GetBalanceByIdQueryRequest,GetBalanceByIdQueryResponse>
    {
        private readonly ICardRepository _cardRepository;

        public GetBalanceByIdQueryHandler(ICardRepository cardRepository)
        { 
            _cardRepository = cardRepository;
        }

        public async Task<GetBalanceByIdQueryResponse> Handle(GetBalanceByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetBalanceByIdAsync(request.CardId, cancellationToken);
            if (card == null)
            {
                throw new Exception($"Card ({request.CardId}) cannot found !");
            }
            return new GetBalanceByIdQueryResponse
            {
                CardId = request.CardId,
                Balance = card
            };

        }
    }
}
