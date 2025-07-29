using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.DecreaseBalance
{
    public class DecreaseBalanceCommandHandler : IRequestHandler<DecreaseBalanceCommandRequest,DecreaseBalanceCommandResponse>
    {
        private readonly ICardRepository _cardRepository;

        public DecreaseBalanceCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<DecreaseBalanceCommandResponse> Handle(DecreaseBalanceCommandRequest request, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.DecreaseBalanceAsync(request.CardId, request.Amount,cancellationToken);

            var response = new DecreaseBalanceCommandResponse 
            {
                CardId = card.Id,
                NewBalance = card.Balance,
                Message = "Balance decreased successfully.",
                Status = true
            };
            return response;
        }

    }
}
