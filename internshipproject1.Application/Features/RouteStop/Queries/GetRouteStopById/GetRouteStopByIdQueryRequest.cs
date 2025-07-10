using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.RouteStop.Queries.GetRouteStopById
{
    public class GetRouteStopByIdQueryRequest : IRequest<GetRouteStopByIdQueryResponse>
    {
        public int Id { get; set; }

        public GetRouteStopByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
