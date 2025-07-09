using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Route.Queries.getRoutesById
{
    public class getRoutesByIdRequest : IRequest<getRoutesByIdResponse>
    {
        public int Id { get; set; }
        public getRoutesByIdRequest(int id)
        {
            Id = id;
        }
    }
}
