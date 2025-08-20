using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Commands.DeleteMenu
{
    public class DeleteMenuCommandResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public DeleteMenuCommandResponse(int id, string message, bool isSuccess)
        {
            Id = id;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
