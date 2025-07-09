using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using MediatR;
namespace internshipproject1.Application.Features.Route.Commands.createRoute
{
    public class createRouteCommand : IRequest<createRouteCommandResponse>
    {
       public string Name { get; set; }
       public string Description { get; set; }
       public string StartLocation { get; set; }
       public string EndLocation { get; set; }
    }
}
