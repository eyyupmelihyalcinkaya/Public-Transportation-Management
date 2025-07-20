using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Queries.GetAllTrips
{
    public class GetAllTripsQueryRequest : IRequest<List<GetAllTripsQueryResponse>>
    {
    }
}
