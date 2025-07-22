using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Features.Card.Queries.IsCardExist;
namespace internshipproject1.Application.Features.Card.Queries.IsCardExist
{
    public class IsCardExistQueryResponse
    {
        public bool IsExist { get; set; }
        public int CustomerId { get; set; }
    }
}
