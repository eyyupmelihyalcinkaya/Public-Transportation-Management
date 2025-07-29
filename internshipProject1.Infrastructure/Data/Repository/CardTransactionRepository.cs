using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using internshipProject1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipProject1.Infrastructure.Data.Repository
{
    public class CardTransactionRepository : ICardTransaction
    {
        private readonly AppDbContext _context;

        public CardTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CardTransaction> AddAsync(CardTransaction cardTransaction, CancellationToken cancellationToken)
        {
            if (cardTransaction == null)
            {
                throw new ArgumentNullException(nameof(cardTransaction), "CardTransaction cannot be null");
            }
            await _context.CardTransaction.AddAsync(cardTransaction, cancellationToken);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e) {
                throw new Exception();
            }
            
            return cardTransaction;
        }

        public async Task<bool> CardTransactionExistsAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Card ID must be greater than zero.");
            }
            var exists = await _context.CardTransaction.AnyAsync(c => c.Id == id, cancellationToken);
            return exists;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var cardTransaction = await _context.CardTransaction.FindAsync(id, cancellationToken);
            if (cardTransaction == null)
            {
                throw new KeyNotFoundException($"CardTransaction with ID {id} not found.");
            }
            cardTransaction.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        public async Task DeleteAsync(CardTransaction entity, CancellationToken cancellationToken)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "CardTransaction cannot be null");
            }
            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        public async Task<IReadOnlyList<CardTransaction>> GetAllAsync(CancellationToken cancellationToken)
        {
            var cardTransactions = await _context.CardTransaction
                .Where(ct => !ct.IsDeleted)
                .ToListAsync(cancellationToken);
            return cardTransactions;
        }

        public async Task<CardTransaction> GetByCardIdAsync(int cardId, CancellationToken cancellationToken)
        {
            if(cardId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cardId), "Card ID must be greater than zero.");
            }
            var cardTransaction = await _context.CardTransaction
                .Where(ct => ct.CardId == cardId && !ct.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (cardTransaction == null)
            {
                throw new KeyNotFoundException($"CardTransaction with Card ID {cardId} not found.");
            }
            return cardTransaction;
        }

        public async Task<CardTransaction> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var cardTransaction = await _context.CardTransaction
                .Where(ct => ct.Id == id && !ct.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (cardTransaction == null)
            { 
                throw new KeyNotFoundException($"CardTransaction with ID {id} not found.");
            }
            return cardTransaction;
        }

        public async Task<CardTransaction> UpdateAsync(CardTransaction cardTransaction, CancellationToken cancellationToken)
        {
            var existingTransaction = await _context.CardTransaction
                .FirstOrDefaultAsync(ct => ct.Id == cardTransaction.Id && !ct.IsDeleted, cancellationToken);
            if (existingTransaction == null)
            {
                throw new KeyNotFoundException($"CardTransaction with ID {cardTransaction.Id} not found.");
            }
            existingTransaction.Amount = cardTransaction.Amount;
            existingTransaction.TransactionDate = cardTransaction.TransactionDate;
            existingTransaction.VehicleType = cardTransaction.VehicleType;
            existingTransaction.Description = cardTransaction.Description;
            existingTransaction.CardId = cardTransaction.CardId;
            existingTransaction.Id = cardTransaction.Id;
            await _context.SaveChangesAsync(cancellationToken);
            return existingTransaction;
        }

        Task IGenericRepository<CardTransaction>.UpdateAsync(CardTransaction entity, CancellationToken cancellationToken)
        {
            return UpdateAsync(entity, cancellationToken);
        }
    }
}
