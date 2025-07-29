using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.UpdateBalance
{
    public class UpdateBalanceCommandRequest : IRequest<UpdateBalanceCommandResponse> 
    {
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncrease { get; set; }
        public UpdateBalanceCommandRequest(int cardId, decimal amount, bool isIncrease)
        {
            CardId = cardId;
            Amount = amount;
            IsIncrease = isIncrease;
        }
    }
}
