using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Commands.AddMenu
{
    public class AddMenuCommandRequest : IRequest<AddMenuCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public int? ParentMenuId { get; set; }

        public AddMenuCommandRequest(int id,string name, string url, int displayOrder, int? parentMenuId)
        {
            Id = id;
            Name = name;
            Url = url;
            DisplayOrder = displayOrder;
            ParentMenuId = parentMenuId;
        }
    }
}
