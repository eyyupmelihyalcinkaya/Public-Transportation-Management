using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.IncreaseBalance
{
    public class IncreaseBalanceCommandRequest : IRequest<IncreaseBalanceCommandResponse>
    {
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public IncreaseBalanceCommandRequest(int cardId, decimal amount)
        {
            CardId = cardId;
            Amount = amount;
        }
    }
}
