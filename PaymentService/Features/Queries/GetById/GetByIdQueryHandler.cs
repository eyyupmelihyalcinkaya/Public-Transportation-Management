using MediatR;
using PaymentService.Features.Queries.GetByCardId;
using PaymentService.Interfaces;

namespace PaymentService.Features.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest,GetByIdQueryResponse>
    {
        private readonly IBoardingTransactionRepository _boardingRepository;

        public GetByIdQueryHandler(IBoardingTransactionRepository boardingRepository)
        {
            _boardingRepository = boardingRepository;
        }

        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellation)
        {
            var boardingTransaction = await _boardingRepository.GetByIdAsync(request.Id, cancellation);
            if (boardingTransaction == null)
            { 
                throw new KeyNotFoundException($"Boarding transaction with ID {request.Id} not found.");
            }
            var response = new GetByIdQueryResponse
            {
                Id = boardingTransaction.Id,
                CardId = boardingTransaction.CardId,
                Amount = boardingTransaction.Amount,
                Balance = boardingTransaction.Balance,
                TransactionDate = boardingTransaction.TransactionDate,
                Status = boardingTransaction.Status
            };
            if (response == null)
            { 
                throw new Exception("Failed to retrieve boarding transaction details.");
            }
            return response;
        }
    }
}
