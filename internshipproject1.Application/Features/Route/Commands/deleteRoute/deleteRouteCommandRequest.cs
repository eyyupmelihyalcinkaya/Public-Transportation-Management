using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Route.Commands.DeleteRoute
{
    public class DeleteRouteCommandRequest : IRequest<DeleteRouteCommandResponse>
    {
        public int Id { get; set; }
    }
}
