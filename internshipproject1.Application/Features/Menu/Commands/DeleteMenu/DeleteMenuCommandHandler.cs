using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Commands.DeleteMenu
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommandRequest, DeleteMenuCommandResponse>
    {
        private readonly IMenuRepository _menuRepository;
        public DeleteMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<DeleteMenuCommandResponse> Handle(DeleteMenuCommandRequest request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdAsync(request.Id, cancellationToken);
            if (menu == null)
            {
                return new DeleteMenuCommandResponse(request.Id, "Menu not found", false);
            }
            await _menuRepository.DeleteAsync(menu.Id, cancellationToken);
            return new DeleteMenuCommandResponse(request.Id, "Menu deleted successfully", true);
        }
    }
}
