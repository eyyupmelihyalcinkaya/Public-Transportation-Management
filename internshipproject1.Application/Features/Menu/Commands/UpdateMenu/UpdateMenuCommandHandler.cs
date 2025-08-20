using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Commands.UpdateMenu
{
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommandRequest, UpdateMenuCommandResponse>
    {
        private readonly IMenuRepository _menuRepository;

        public UpdateMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<UpdateMenuCommandResponse> Handle(UpdateMenuCommandRequest request, CancellationToken cancellationToken)
        { 
            var updatedMenu = await _menuRepository.GetByIdAsync(request.Id, cancellationToken);
            if (updatedMenu == null)
            {
                return new UpdateMenuCommandResponse
                {
                    IsSuccess = false,
                    Message = "Menu not found"
                };
            }
            updatedMenu.Name = request.Name;
            updatedMenu.Url = request.Url;
            updatedMenu.DisplayOrder = request.DisplayOrder;
            updatedMenu.ParentMenuId = request.ParentMenuId;
            await _menuRepository.UpdateAsync(updatedMenu, cancellationToken);
            return new UpdateMenuCommandResponse
            {
                Id = updatedMenu.Id,
                Name = updatedMenu.Name,
                Url = updatedMenu.Url,
                DisplayOrder = updatedMenu.DisplayOrder,
                ParentMenuId = updatedMenu.ParentMenuId,
                Message = "Menu updated successfully",
                IsSuccess = true
            };
        }
    }
}
