using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.AddCard
{
    public class AddCardCommandResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
