using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;
using MediatR;
namespace internshipproject1.Application.Features.Stop.Queries.getStopById
{
    public class getStopByIdQueryRequest : IRequest<List<getStopByIdQueryResponse>>
    {
        public int Id { get; set; }

        public getStopByIdQueryRequest(int id) { 
            Id = id;
        }


    }
}
