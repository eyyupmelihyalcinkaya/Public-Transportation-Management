using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommandRequest, UpdateCardCommandResponse>
    {
        private readonly ICardRepository _cardRepository;

        public UpdateCardCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<UpdateCardCommandResponse> Handle(UpdateCardCommandRequest request, CancellationToken cancellationToken)
        {
            var existingCard = await _cardRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingCard == null)
            {
                return new UpdateCardCommandResponse
                {
                    Message = $"Card {request.Id} Cannot Updated, Because the Id was not found!",
                    IsSuccess = false
                };
            }
            existingCard.Id = request.Id;
            existingCard.Balance = request.Balance;
            existingCard.ExpirationDate = request.ExpirationDate;
            await _cardRepository.UpdateAsync(existingCard, cancellationToken);
            return new UpdateCardCommandResponse
            {
                Id = request.Id,
                IsSuccess = true,
                Message = $"Card {request.Id} Updated Successfully"
            };

        }
    }
}
