using MediatR;
using PaymentService.Interfaces;

namespace PaymentService.Features.Queries.GetByCardId
{
    public class GetByCardIdQueryHandler : IRequestHandler<GetByCardIdQueryRequest, GetByCardIdQueryResponse>
    {
        private readonly IBoardingTransactionRepository _boardingRepository;

        public GetByCardIdQueryHandler(IBoardingTransactionRepository boardingRepository)
        {
            _boardingRepository = boardingRepository;
        }

        public async Task<GetByCardIdQueryResponse> Handle(GetByCardIdQueryRequest request, CancellationToken cancellationToken)
        { 
            var transactions = await _boardingRepository.GetByCardIdAsync(request.CardNumber, cancellationToken);
            if (transactions == null || !transactions.Any())
            {
                throw new KeyNotFoundException($"No transactions found for CardId: {request.CardNumber}");
            }
            var response = transactions.Select(t => new GetByCardIdQueryResponse
            {
                Id = t.Id,
                CardNumber = t.CardNumber,
                Amount = t.Amount,
                Balance = t.Balance,
                TransactionDate = t.TransactionDate,
                Status = t.Status
            }).FirstOrDefault();
            if (response == null)
            {
                throw new InvalidOperationException("Failed to retrieve transaction details");
            }
            return response;
        }
    }
}
