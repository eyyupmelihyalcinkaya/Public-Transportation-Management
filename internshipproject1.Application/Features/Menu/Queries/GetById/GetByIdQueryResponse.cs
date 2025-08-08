using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Queries.GetById
{
    public class GetByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public int? ParentMenuId { get; set; }
    }
}
