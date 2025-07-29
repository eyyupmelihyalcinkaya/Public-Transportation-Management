using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandHandler
    : IRequestHandler<UpdateTransactionCommandRequest, UpdateTransactionCommandResponse>
    {
        private readonly ICardTransaction _cardTransactionRepository;
        private readonly ICardRepository _cardRepository;

        public UpdateTransactionCommandHandler(
            ICardTransaction cardTransactionRepository,
            ICardRepository cardRepository)
        {
            _cardTransactionRepository = cardTransactionRepository;
            _cardRepository = cardRepository;
        }

        public async Task<UpdateTransactionCommandResponse> Handle(
            UpdateTransactionCommandRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _cardTransactionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (transaction == null)
            {
                return new UpdateTransactionCommandResponse
                {
                    IsSuccess = false,
                    Message = "Transaction not found"
                };
            }
            if (transaction.CardId != request.CardId)
            {
                var newCard = await _cardRepository.GetByIdAsync(request.CardId, cancellationToken);
                if (newCard == null)
                {
                    return new UpdateTransactionCommandResponse
                    {
                        IsSuccess = false,
                        Message = "New card not found"
                    };
                }
                transaction.CardId = request.CardId;
            }
            transaction.Amount = request.Amount;
            transaction.TransactionDate = request.TransactionDate;
            transaction.VehicleType = request.VehicleType;
            await _cardTransactionRepository.UpdateAsync(transaction, cancellationToken);
            return new UpdateTransactionCommandResponse
            {
                Id = transaction.Id,
                CardId = transaction.CardId,
                VehicleType = transaction.VehicleType,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                Message = "Transaction updated successfully",
                IsSuccess = true
            };
        }
    }

}
