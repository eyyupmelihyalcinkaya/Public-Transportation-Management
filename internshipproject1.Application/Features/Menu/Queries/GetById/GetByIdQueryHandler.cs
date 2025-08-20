using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        private readonly IMenuRepository _menuRepository;
    
        public GetByIdQueryHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdAsync(request.Id, cancellationToken);
            if (menu == null)
            {
                throw new KeyNotFoundException($"Menu with ID {request.Id} not found.");
            }
            return new GetByIdQueryResponse
            {
               Id = menu.Id,
               Name = menu.Name,
               Url = menu.Url,
               DisplayOrder = menu.DisplayOrder,
               ParentMenuId = menu.ParentMenuId
            };
        }
    }
}
