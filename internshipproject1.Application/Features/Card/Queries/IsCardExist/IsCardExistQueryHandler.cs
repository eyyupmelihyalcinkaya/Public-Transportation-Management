using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Application.Features.Card.Queries.IsCardExist;
namespace internshipproject1.Application.Features.Card.Queries.IsCardExist
{
    public class IsCardExistQueryHandler : IRequestHandler<IsCardExistQueryRequest, IsCardExistQueryResponse>
    {
        private readonly ICardRepository _cardRepository;

        public IsCardExistQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<IsCardExistQueryResponse> Handle(IsCardExistQueryRequest request, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetByIdAsync(request.Id, cancellationToken);
            if(card == null)
            {
                return new IsCardExistQueryResponse { 
                    IsExist = false
                };
            }
            return new IsCardExistQueryResponse
            {
                IsExist = true,
                CustomerId = card.CustomerId
            };
        }
    }
}
