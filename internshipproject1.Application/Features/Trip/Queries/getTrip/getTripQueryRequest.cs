using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using internshipproject1.Domain.Entities;

namespace internshipproject1.Application.Features.Trip.Queries.GetTrip
{
    public class GetTripQueryRequest : IRequest<List<GetTripQueryResponse>>
    {
        public int Id { get; set; }
      
        public GetTripQueryRequest(int id)
        {
            Id = id;
        }
    }
}
