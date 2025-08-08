using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using MediatR;
namespace internshipproject1.Application.Features.Card.Commands.AddCard
{
    public class AddCardCommandHandler : IRequestHandler<AddCardCommandRequest, AddCardCommandResponse>
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICustomerRepository _customerRepository;
        public AddCardCommandHandler(ICardRepository cardRepository, ICustomerRepository customerRepository)
        {
            _cardRepository = cardRepository;
            _customerRepository = customerRepository;
        }
        public async Task<AddCardCommandResponse> Handle(AddCardCommandRequest request, CancellationToken cancellationToken)
        {
            
            var customer = await _customerRepository.GetByEmailAsync(request.CustomerEmail, cancellationToken);
            var isExists = await _cardRepository.CardExistsAsync(customer.Id, cancellationToken);
            if (isExists)
            { 
                throw new Exception("Card already exists for this customer.");
            }
            /*if (customer == null)
            {
                return new AddCardCommandResponse
                {
                    Success = false,
                    Message = "Customer not found."
                };
            }*/


            var createdCard = new Domain.Entities.Card {
                CustomerId = customer.Id,
                CardNumber = GenerateCardNumber(),
                Balance = request.Balance,
                ExpirationDate = request.ExpirationDate,
                IsActive = request.IsActive,
                IsDeleted = request.IsDeleted
            };
            var result = await _cardRepository.AddAsync(createdCard, cancellationToken);
          //  var cutomerCardList = await _customerRepository.AddCardToListAsync(customer.Id, createdCard, cancellationToken);
            if (result == null)
            {
                return new AddCardCommandResponse
                {
                    Success = false,
                    Message = "Card creation failed."
                };
            }
            return new AddCardCommandResponse
            {
                Success = true,
                Message = "Card created successfully.",
                Id = result.Id,
                CustomerId = result.CustomerId,
                CardNumber = result.CardNumber,
            };
        }
        public string GenerateBaseCardNumber(int length)
        {
            var random = new Random();
            var digits = new char[length];
            digits[0] = '2';
            for (int i = 1; i < length; i++)
            {
                digits[i] = (char)('0' + random.Next(0, 10));
            }
            return new string(digits);
        }

        public char CalculateLuhnCheck(string CardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = CardNumber.Length - 1; i >= 0; i--)
            {
                int digit = CardNumber[i] - '0';
                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit = digit % 10 + 1;
                    }
                }
                sum += digit;
                alternate = !alternate;
            }
            return (char)('0' + (10 - (sum % 10)) % 10);
        }

        public string FormatCardNumber(string CardNumber)
        {
            return string.Join("",
                CardNumber.Substring(0, 4),
                CardNumber.Substring(4, 4),
                CardNumber.Substring(8, 4),
                CardNumber.Substring(12, 4)
                );
        }

        public string GenerateCardNumber()
        {
            string baseNum = GenerateBaseCardNumber(15);
            char LuhnDigit = CalculateLuhnCheck(baseNum);
            string FullNumber = baseNum + LuhnDigit;
            return FormatCardNumber(FullNumber);
        }
    
    }

   
}
