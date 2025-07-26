using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommandRequest, DeleteTransactionCommandResponse>
    {
        private readonly ICardTransaction _cardTransactionRepository;
        public DeleteTransactionCommandHandler(ICardTransaction cardTransactionRepository)
        {
            _cardTransactionRepository = cardTransactionRepository;
        }
        public async Task<DeleteTransactionCommandResponse> Handle(DeleteTransactionCommandRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _cardTransactionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (transaction == null)
            {
                return new DeleteTransactionCommandResponse
                {
                    Message = "Transaction not found.",
                    IsSuccess = false
                };
            }
            await _cardTransactionRepository.DeleteAsync(transaction, cancellationToken);
            return new DeleteTransactionCommandResponse
            {
                Message = "Card transaction deleted successfully.",
                IsSuccess = true
            };
        }
    }
}
