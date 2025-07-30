using internshipproject1.Application.DTOs;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Application.Interfaces.Services;
using internshipProject1.Infrastructure.Context;
using internshipproject1.Domain.Enums;
using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipProject1.Infrastructure.Data.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly ICardRepository _cardRepository;
        private readonly ICardTransaction _cardTransaction;
        public PaymentService(AppDbContext context,ICardRepository cardRepository,ICardTransaction cardTransaction)
        {
            _context = context;
            _cardRepository = cardRepository;
            _cardTransaction = cardTransaction;
        }

        public async Task ProcessPaymentAsync(PaymentEventDTO dto,CancellationToken cancellationToken)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "PaymentEventDTO cannot be null");
            }
            var card = await _cardRepository.GetByIdAsync(dto.CardId, cancellationToken);
            if (card == null)
            {
                throw new KeyNotFoundException($"Card with ID {dto.CardId} not found.");
            }
            var transaction = new CardTransaction();
            switch (dto.TransactionType)
            {
                case TransactionType.Deposit:
                    card = await _cardRepository.IncreaseBalanceAsync(dto.CardId, dto.Amount, cancellationToken);
                   var DepositTransaction = new CardTransaction
                    {
                        Amount = dto.Amount,
                        CardId = dto.CardId,
                        TransactionType = dto.TransactionType,
                        TransactionDate = DateTime.UtcNow,
                        Balance = card.Balance
                    };
                    await _cardTransaction.AddAsync(DepositTransaction, cancellationToken);
                    break;
                case TransactionType.Withdraw:
                case TransactionType.Payment:
                    card = await _cardRepository.DecreaseBalanceAsync(dto.CardId, dto.Amount, cancellationToken);
                    var WithdrawTransaction = new CardTransaction
                    {
                        Amount = dto.Amount,
                        CardId = dto.CardId,
                        TransactionType = dto.TransactionType,
                        TransactionDate = DateTime.UtcNow,
                        Balance = card.Balance
                    };
                    await _cardTransaction.AddAsync(WithdrawTransaction, cancellationToken);
                    break;
            }
        }
    }
}
