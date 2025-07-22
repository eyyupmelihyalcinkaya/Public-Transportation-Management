using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.AddCard
{
    public class AddCardCommandRequest : IRequest<AddCardCommandResponse>
    {
        public string CustomerEmail { get; set; }
        public decimal Balance { get; set; } = 0.0m;
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

    }
}
