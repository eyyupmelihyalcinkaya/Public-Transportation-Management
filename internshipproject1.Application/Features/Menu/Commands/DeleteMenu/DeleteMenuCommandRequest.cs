using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Commands.DeleteMenu
{
    public class DeleteMenuCommandRequest : IRequest<DeleteMenuCommandResponse>
    {
        public int Id { get; set; }
        public DeleteMenuCommandRequest(int id)
        {
            Id = id;
        }
    }
}
