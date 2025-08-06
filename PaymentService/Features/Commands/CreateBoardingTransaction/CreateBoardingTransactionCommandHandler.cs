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
                CardNumber = request.CardNumber,
                UserId = request.UserId,
                Amount = request.Amount,
                TransactionType = request.TransactionType,
                VehicleType = request.VehicleType,
                TransactionDate = request.TransactionDate,
                Status = request.Status,
                isStudent = request.isStudent
            };
            var createdTransaction = await _boardingRepository.AddAsync(transaction, cancellationToken);
            if (createdTransaction == null)
            {
                throw new InvalidOperationException("Failed to create boarding transaction");
            }
            return new CreateBoardingTransactionCommandResponse
            {
                Id = createdTransaction.Id,
                CardNumber = createdTransaction.CardNumber,
                UserId = createdTransaction.UserId,
                UserName = request.UserName, 
                Amount = createdTransaction.Amount,
                TransactionType = createdTransaction.TransactionType,
                VehicleType = createdTransaction.VehicleType,
                TransactionDate = createdTransaction.TransactionDate,
                Message = "Boarding transaction created successfully",
                Status = createdTransaction.Status,
                isStudent = createdTransaction.isStudent
            };

        }
    }
}
