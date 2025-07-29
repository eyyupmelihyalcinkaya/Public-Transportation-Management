using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Repositories
{
    /*
        ICardTransaction
        Best Practice: Transaction işlemleri için gerekli metotlar var.
        Eksik/Yanlış:
        GetByCardIdAsync tek transaction döndürüyor, bir karta birden fazla transaction olabilir. Liste dönmeli.
        DeleteAsync parametresi int id, ama handler’da entity bekleniyor (uyumsuzluk olabilir)
     
     */
    public interface ICardTransaction : IGenericRepository<CardTransaction>
    {
        public Task<CardTransaction> GetByIdAsync(int id, CancellationToken cancellationToken); //query
        public Task<CardTransaction> GetByCardIdAsync(int cardId, CancellationToken cancellationToken); //query
        public Task<CardTransaction> AddAsync(CardTransaction cardTransaction, CancellationToken cancellationToken); //command
        public Task<CardTransaction> UpdateAsync(CardTransaction cardTransaction, CancellationToken cancellationToken); // command
        public Task DeleteAsync(int id, CancellationToken cancellationToken); // command
        public Task<bool> CardTransactionExistsAsync(int cardId, CancellationToken cancellationToken); // query
        public Task<IReadOnlyList<CardTransaction>> GetAllAsync(CancellationToken cancellationToken); // query

    }
}
