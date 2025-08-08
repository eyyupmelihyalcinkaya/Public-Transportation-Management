using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Repositories
{
    /*
            ICardRepository
            Best Practice: CRUD ve ek fonksiyonlar (aktif/pasif kartlar, müşteri bazlı arama) var.
            Eksik/Yanlış:
            AddAsync, UpdateAsync ve DeleteAsync metotları hem burada hem generic interface’te olabilir, tekrar olabilir.
            DeleteAsync gerçek silme mi yoksa soft delete mi? CQRS handler’da soft delete uygulanıyor, interface’de açıklama yok.
            GetByCustomerIdAsync birden fazla kart dönebilir, tek kart dönüyor. Müşterinin birden fazla kartı olamaz mı?
            İsimlendirme: Tutarlı ve anlaşılır.
     
     */
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
        Task<Card> UpdateCardBalanceAsync(int cardId, decimal newBalance, CancellationToken cancellationToken);
        Task<decimal> GetBalanceByIdAsync(int cardId, CancellationToken cancellationToken); //query
        Task<Card> IncreaseBalanceAsync(int cardId,decimal amount , CancellationToken cancellationToken);
        Task<Card> DecreaseBalanceAsync(int cardId,decimal amount, CancellationToken cancellationToken);
        Task<IEnumerable<Card>> GetCardsOrderedByBalanceAsync(CancellationToken cancellationToken); //query
        Task<IEnumerable<Card>> GetCardsByBalanceRangeAsync(decimal minRange, decimal maxRange, CancellationToken cancellationToken); //query
        Task<Card> GetCardIdByCardNumber(string cardNumber, CancellationToken cancellationToken);
        Task<decimal> GetBalanceByCardNumberAsync(string cardNumber, CancellationToken cancellationToken);
        Task<Card> GetCardByCustomerEmail(string email, CancellationToken cancellationToken);
    
    }
}
