using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.UpdateCard
{
    public class UpdateCardCommandResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
