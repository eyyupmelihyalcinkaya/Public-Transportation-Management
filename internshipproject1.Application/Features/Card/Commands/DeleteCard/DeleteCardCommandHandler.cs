using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.DeleteCard
{
    public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommandRequest,DeleteCardCommandResponse>
    {
        private readonly ICardRepository _cardRepository;

        public DeleteCardCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<DeleteCardCommandResponse> Handle(DeleteCardCommandRequest request, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetByIdAsync(request.Id, cancellationToken);
            if (card == null)
            {
                return new DeleteCardCommandResponse
                {
                    Id = request.Id,
                    Message = "Card not found",
                    IsSuccess = false
                };
            }
            card.IsActive = false;
            card.IsDeleted = true;
            await _cardRepository.UpdateAsync(card, cancellationToken);
            return new DeleteCardCommandResponse 
            {
                Message = "Card deleted successfully",
                IsSuccess = true
            };
        }
    }
}
