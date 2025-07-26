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
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;
        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Card> AddAsync(Card card, CancellationToken cancellationToken)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card), "Card cannot be null");
            }
            await _context.Card.AddAsync(card, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return card;
        }

        public async Task<bool> CardExistsAsync(int customerId, CancellationToken cancellationToken)
        {
            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId), "Customer ID must be greater than zero.");
            }
            var exists = await _context.Card.AnyAsync(c => c.CustomerId == customerId, cancellationToken);
            return exists;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var card = await _context.Card.FindAsync(id, cancellationToken);
            card.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        public async Task DeleteAsync(Card entity, CancellationToken cancellationToken)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Card cannot be null");
            }
            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }
        public async Task<IReadOnlyList<Card>> GetAllActiveCardsAsync(CancellationToken cancellationToken)
        {
            var activeCards = await _context.Card.Where(c=>c.IsActive && !c.IsDeleted).ToListAsync(cancellationToken);
            return activeCards;
        }

        public async Task<IReadOnlyList<Card>> GetAllAsync(CancellationToken cancellationToken)
        {
            var AllCards = await _context.Card.Where(c=>!c.IsDeleted).ToListAsync(cancellationToken);
            return AllCards;
        }

        public async Task<IReadOnlyList<Card>> GetAllInactiveCardsAsync(CancellationToken cancellationToken)
        {
            var inactiveCards = await _context.Card.Where(c => !c.IsActive && !c.IsDeleted).ToListAsync(cancellationToken);
            return inactiveCards;
        }

        public async Task<Card> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
        {
            var card = await _context.Card.Where(c=>c.CustomerId == customerId && !c.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (card == null)
                {
                throw new KeyNotFoundException($"Card with Customer ID {customerId} not found.");
            }
            return card;
        }

        public async Task<Card> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var card = await _context.Card.FindAsync(id, cancellationToken);
            if (card == null || card.IsDeleted)
            {
                throw new KeyNotFoundException($"Card with ID {id} not found or has been deleted.");
            }
            return card;
        }

        public async Task<Card> UpdateAsync(Card card, CancellationToken cancellationToken)
        {
            var existingCard = await _context.Card.FindAsync(card.Id, cancellationToken);
            if (existingCard == null)
            {
                throw new KeyNotFoundException($"Card with ID {card.Id} not found or has been deleted.");
            }
            existingCard.Balance = card.Balance;
            existingCard.ExpirationDate = card.ExpirationDate;
            existingCard.IsActive = card.IsActive;
            _context.Card.Update(existingCard);
            await _context.SaveChangesAsync(cancellationToken);
            return existingCard;
        }
        Task IGenericRepository<Card>.UpdateAsync(Card entity, CancellationToken cancellationToken)
        {
            return UpdateAsync(entity, cancellationToken);
        }
    }
}
