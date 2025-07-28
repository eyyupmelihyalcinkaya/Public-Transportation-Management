using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.UpdateBalance
{
    public class UpdateBalanceCommandHandler : IRequestHandler<UpdateBalanceCommandRequest, UpdateBalanceCommandResponse>
    {
        private readonly ICardRepository _cardRepository;

        public UpdateBalanceCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<UpdateBalanceCommandResponse> Handle(UpdateBalanceCommandRequest request,CancellationToken cancellationToken)
        { 
            var card = await _cardRepository.DecreaseBalanceAsync(request.CardId, request.Amount, cancellationToken);
            var response = new UpdateBalanceCommandResponse
            {
                CardId = card.Id,
                NewBalance = card.Balance,
                IsIncrease = request.IsIncrease,
                Status = true,
                Message = request.IsIncrease 
                    ? $"Card ({card.Id}) balance increased by {request.Amount}." 
                    : $"Card ({card.Id}) balance decreased by {request.Amount}."
            };
            return response;
        }
    }
}
