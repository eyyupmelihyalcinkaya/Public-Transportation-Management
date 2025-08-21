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
using DnsClient.Internal;

namespace internshipProject1.Infrastructure.Data.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly ICardRepository _cardRepository;
        private readonly ICardTransaction _cardTransaction;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        public PaymentService(AppDbContext context,ICardRepository cardRepository,ICardTransaction cardTransaction,IUserRepository userRepository,ICustomerRepository customerRepository)
        {
            _context = context;
            _cardRepository = cardRepository;
            _cardTransaction = cardTransaction;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        public async Task ProcessPaymentAsync(PaymentEventDTO dto,CancellationToken cancellationToken)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "PaymentEventDTO cannot be null");
            }
            //var card = await _cardRepository.GetByIdAsync(dto.CardId, cancellationToken);
            var card = await _cardRepository.GetCardIdByCardNumber(dto.CardNumber, cancellationToken);
            if (card == null)
            { 
                throw new KeyNotFoundException(nameof(card));
            }
            var CardId = card.Id;
            if (CardId == 0)
            { 
                throw new ArgumentException(nameof(card));
            }
            //TODO : Burada bi dümen çevirip dto içine userID'yi vermem lazım. --- Bİ DÜMEN YAPTIM

            //TODO: Burada ikinci bi dümen şart oldu, bizim card ID'lerden baaaaazıları araaazi olmuş onları cardnumber yapacaz. --- BABA İKİNCİ DÜMENİ DE YAPTI

            if (card == null)
            {
                throw new KeyNotFoundException($"Card with ID {CardId} not found.");
            }
            
            var user = await _userRepository.GetByIdWithCustomerAsync(dto.UserId, cancellationToken);
            
            if(user == null)
            {
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");
            }
            
            var customer = user.Customer;
            var customerCard = customer.CardList.FirstOrDefault(c => c.CardNumber == card.CardNumber);
            customer.Card = customerCard;
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with User ID {dto.UserId} not found.");
            }
            
            var transaction = new CardTransaction();
            
            var CustomersCard = customer.Card;
            
            if (CustomersCard == null)
            {
                throw new KeyNotFoundException($"Customer with User ID {dto.UserId} does not have a card.");
            }

            if (card.Id != CustomersCard.Id)
            {
                throw new InvalidOperationException("Card does not belong to the customer.");
            }
            switch (dto.TransactionType)
            {
                case TransactionType.Deposit:
                    card = await _cardRepository.IncreaseBalanceAsync(CardId, dto.Amount, cancellationToken);
                    
                    var DepositTransaction = new CardTransaction
                    {
                        Amount = dto.Amount,
                        CardId = CardId,
                        UserId = dto.UserId,
                        CustomerId = customer.Id,
                        TransactionType = dto.TransactionType,
                        TransactionDate = DateTime.UtcNow,
                        Balance = card.Balance
                    };
                    await _cardTransaction.AddAsync(DepositTransaction, cancellationToken);
                    break;
                case TransactionType.Withdraw:
                case TransactionType.Payment:
                    if (dto.isStudent)
                    { 
                        dto.Amount = dto.Amount * 0.5m; // Apply student discount
                        card = await _cardRepository.IncreaseBalanceAsync(CardId, dto.Amount, cancellationToken);
                        var StudentTransaction = new CardTransaction
                        {
                            Amount = dto.Amount,
                            CardId = CardId,
                            UserId = dto.UserId,
                            CustomerId = customer.Id,
                            TransactionType = dto.TransactionType,
                            TransactionDate = DateTime.UtcNow,
                            Balance = card.Balance
                        };
                        await _cardTransaction.AddAsync(StudentTransaction, cancellationToken);
                        break;
                    }
                    card = await _cardRepository.DecreaseBalanceAsync(CardId, dto.Amount, cancellationToken);
                    
                    var WithdrawTransaction = new CardTransaction
                    {
                        Amount = dto.Amount,
                        CardId = CardId,
                        UserId = dto.UserId,
                        CustomerId = customer.Id,
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
