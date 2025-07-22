using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.UpdateCard
{
    public class UpdateCardCommandRequest : IRequest<UpdateCardCommandResponse>
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
