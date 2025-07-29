using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommandRequest, CreateTransactionCommandResponse>
    {
        private readonly ICardTransaction _cardTransactionRepository;
        public CreateTransactionCommandHandler(ICardTransaction cardTransactionRepository)
        {
            _cardTransactionRepository = cardTransactionRepository;
        }
        public async Task<CreateTransactionCommandResponse> Handle(CreateTransactionCommandRequest request, CancellationToken cancellationToken)
        { 
            var cardTransaction = new Domain.Entities.CardTransaction
            {
                CardId = request.CardId,
                TransactionDate = request.TransactionDate,
                Amount = request.Amount,
                VehicleType = request?.VehicleType,
            };
            if (cardTransaction == null)
            {
                return new CreateTransactionCommandResponse
                {
                    Message = "Transaction cannot created.",
                    IsSuccess = false
                };
            }
            await _cardTransactionRepository.AddAsync(cardTransaction, cancellationToken);
            return new CreateTransactionCommandResponse
            {
                Id = cardTransaction.Id,
                CardId = cardTransaction.CardId,
                TransactionDate = cardTransaction.TransactionDate,
                Amount = cardTransaction.Amount,
                VehicleType = cardTransaction.VehicleType,
                Message = "Card transaction created successfully.",
                IsSuccess = true
            };
        }
    }
}
