using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardByCustomerEmail
{
    public class GetCardByCustomerEmailQueryHandler : IRequestHandler<GetCardByCustomerEmailQueryRequest, GetCardByCustomerEmailQueryResponse>
    {
        private readonly ICardRepository _cardRepository;

        public GetCardByCustomerEmailQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<GetCardByCustomerEmailQueryResponse> Handle(GetCardByCustomerEmailQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var card = await _cardRepository.GetCardByCustomerEmail(request.Email, cancellationToken);
                
                return new GetCardByCustomerEmailQueryResponse
                {
                    CardNumber = card.CardNumber,
                    CardId = card.Id,
                    Balance = card.Balance
                };
            }
            catch (KeyNotFoundException)
            {
                // Email ile kart bulunamadı durumunda null döndür
                return null;
            }
        }
    }
}
