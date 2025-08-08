using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Commands.AddMenu
{
    public class AddMenuCommandHandler : IRequestHandler<AddMenuCommandRequest, AddMenuCommandResponse>
    {
        private readonly IMenuRepository _menuRepository;
        public AddMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<AddMenuCommandResponse> Handle(AddMenuCommandRequest request, CancellationToken cancellationToken)
        {
            var menu = new Domain.Entities.Menu
            {
                Name = request.Name,
                Url = request.Url,
                DisplayOrder = request.DisplayOrder,
                ParentMenuId = request.ParentMenuId
            };

            var createdMenu = await _menuRepository.AddAsync(menu, cancellationToken);

            return new AddMenuCommandResponse
            {
                Id = createdMenu.Id,
                Name = createdMenu.Name,
                Url = createdMenu.Url,
                DisplayOrder = createdMenu.DisplayOrder,
                ParentMenuId = createdMenu.ParentMenuId
            };
        }
    }
}
