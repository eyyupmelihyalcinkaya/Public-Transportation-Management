using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface ICardRepository : IGenericRepository<Card>
    {
        Task<Card> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Card> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
        Task<Card> AddAsync(Card card, CancellationToken cancellationToken);
        Task<Card> UpdateAsync(Card card, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> CardExistsAsync(int customerId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Card>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<Card>> GetAllActiveCardsAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<Card>> GetAllInactiveCardsAsync(CancellationToken cancellationToken);

    }
}
