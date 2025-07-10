using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;
using MediatR;
namespace internshipproject1.Application.Features.Stop.Queries.GetStopById
{
    public class GetStopByIdQueryRequest : IRequest<List<GetStopByIdQueryResponse>>
    {
        public int Id { get; set; }

        public GetStopByIdQueryRequest(int id) { 
            Id = id;
        }


    }
}
