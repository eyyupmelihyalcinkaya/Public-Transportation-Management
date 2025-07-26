using MediatR;
using PaymentService.Interfaces;

namespace PaymentService.Features.Commands.CreateBoardingTransaction
{
    public class CreateBoardingTransactionCommandHandler : IRequestHandler<CreateBoardingTransactionCommandRequest,CreateBoardingTransactionCommandResponse>
    {
        private readonly IBoardingTransactionRepository _boardingRepository;

        public CreateBoardingTransactionCommandHandler(IBoardingTransactionRepository boardingRepository)
        {
            _boardingRepository = boardingRepository;
        }

        public async Task<CreateBoardingTransactionCommandResponse> Handle(CreateBoardingTransactionCommandRequest request, CancellationToken cancellationToken)
        { 
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            var transaction = new Entities.BoardingTransaction
            {
                CardId = request.CardId,
                Amount = request.Amount,
                Balance = request.Balance,
                TransactionDate = request.TransactionDate,
                Status = request.Status
            };
            var createdTransaction = await _boardingRepository.AddAsync(transaction, cancellationToken);
            if (createdTransaction == null)
            {
                throw new InvalidOperationException("Failed to create boarding transaction");
            }
            return new CreateBoardingTransactionCommandResponse
            {
                Id = createdTransaction.Id,
                CardId = createdTransaction.CardId,
                Amount = createdTransaction.Amount,
                Balance = createdTransaction.Balance,
                TransactionDate = createdTransaction.TransactionDate,
                Status = createdTransaction.Status,
                Message = "Boarding transaction created successfully"
            };

        }
    }
}
