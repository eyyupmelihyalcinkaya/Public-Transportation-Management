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
        public AddCardCommandHandler(ICardRepository cardRepository,ICustomerRepository customerRepository)
        {
            _cardRepository = cardRepository;
            _customerRepository = customerRepository;
        }
        public async Task<AddCardCommandResponse> Handle(AddCardCommandRequest request, CancellationToken cancellationToken)
        {
            // TODO: Melih
            
            var customer = await _customerRepository.GetByEmailAsync(request.CustomerEmail, cancellationToken);
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
                Balance = request.Balance,
                ExpirationDate = request.ExpirationDate,
                IsActive = request.IsActive,
                IsDeleted = request.IsDeleted
                };
            var result = await _cardRepository.AddAsync(createdCard, cancellationToken);
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
            };
        }
    }
}
