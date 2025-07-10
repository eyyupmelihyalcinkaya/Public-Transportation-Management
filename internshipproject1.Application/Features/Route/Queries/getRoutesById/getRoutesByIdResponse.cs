using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Route.Queries.GetRoutesById
{
    public class GetRoutesByIdResponse : IRequest<GetRoutesByIdResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }

    }
}
