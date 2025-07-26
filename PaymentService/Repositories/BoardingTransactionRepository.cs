using Microsoft.EntityFrameworkCore;
using PaymentService.Entities;
using PaymentService.Interfaces;

namespace PaymentService.Repositories
{
    public class BoardingTransactionRepository : IBoardingTransactionRepository
    {
        private readonly PaymentDbContext _context;
        public BoardingTransactionRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public async Task<BoardingTransaction> AddAsync(BoardingTransaction boardingTransaction, CancellationToken cancellationToken)
        {
            var transaction = await _context.BoardingTransactions.AddAsync(boardingTransaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            if (transaction.Entity == null)
            {
                throw new InvalidOperationException("Failed to add boarding transaction.");
            }
            return transaction.Entity;
        }

        public async Task<IEnumerable<BoardingTransaction>> GetByCardIdAsync(int cardId, CancellationToken cancellationToken)
        {
            var transaction = await _context.BoardingTransactions
                .Where(c=>c.CardId == cardId).ToListAsync(cancellationToken);
            if (transaction == null || !transaction.Any())
            {
                throw new KeyNotFoundException($"No transactions found for card ID {cardId}");
            }
            return transaction;
        }

        public async Task<BoardingTransaction> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var transaction = await _context.BoardingTransactions
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transaction with ID {id} not found.");
            }
            return transaction;
        }
    }
}
