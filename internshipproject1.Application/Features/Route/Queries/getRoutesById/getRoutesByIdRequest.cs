using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Route.Queries.GetRoutesById
{
    public class GetRoutesByIdRequest : IRequest<GetRoutesByIdResponse>
    {
        public int Id { get; set; }
        public GetRoutesByIdRequest(int id)
        {
            Id = id;
        }
    }
}
