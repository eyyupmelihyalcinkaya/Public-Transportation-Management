using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface ICardTransaction : IGenericRepository<CardTransaction>
    {
        public Task<CardTransaction> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<CardTransaction> GetByCardIdAsync(int cardId, CancellationToken cancellationToken);
        public Task<CardTransaction> AddAsync(CardTransaction cardTransaction, CancellationToken cancellationToken);
        public Task<CardTransaction> UpdateAsync(CardTransaction cardTransaction, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<bool> CardTransactionExistsAsync(int cardId, CancellationToken cancellationToken);
        public Task<IReadOnlyList<CardTransaction>> GetAllAsync(CancellationToken cancellationToken);

    }
}
