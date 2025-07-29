using PaymentService.Entities;

namespace PaymentService.Interfaces
{
    public interface IBoardingTransactionRepository
    {
        public Task<BoardingTransaction> AddAsync(BoardingTransaction boardingTransaction,CancellationToken cancellationToken);
        public Task<BoardingTransaction> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<BoardingTransaction>> GetByCardIdAsync(int cardId,CancellationToken cancellationToken);
    }
}
