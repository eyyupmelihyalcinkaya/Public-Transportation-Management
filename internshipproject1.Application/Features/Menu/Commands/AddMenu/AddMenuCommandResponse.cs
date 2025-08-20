using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Commands.AddMenu
{
    public class AddMenuCommandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public int? ParentMenuId { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
