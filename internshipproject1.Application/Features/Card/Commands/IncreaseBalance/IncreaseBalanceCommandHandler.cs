using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.IncreaseBalance
{
    //TODO: bakiye işlemlerine old balance new balance datetime falan eklenecek
    public class IncreaseBalanceCommandHandler : IRequestHandler<IncreaseBalanceCommandRequest, IncreaseBalanceCommandResponse>
    {
        private readonly ICardRepository _cardRepository;

        public IncreaseBalanceCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<IncreaseBalanceCommandResponse> Handle(IncreaseBalanceCommandRequest request, CancellationToken cancellationToken)
        { 
            var card = await _cardRepository.IncreaseBalanceAsync(request.CardId, request.Amount, cancellationToken);
            return new IncreaseBalanceCommandResponse
            {
                CardId = card.Id,
                NewBalance = card.Balance,
                Message = $"Card balance increased successfully. New balance: {card.Balance:C}",
                Status = true
            };
        }
    }
}
