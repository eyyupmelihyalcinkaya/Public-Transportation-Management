using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQueryRequest, List<GetAllQueryResponse>>
    {
        private readonly IMenuRepository _menuRepository;

        public GetAllQueryHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<GetAllQueryResponse>> Handle(GetAllQueryRequest request, CancellationToken cancellationToken)
        { 
            var menus = await _menuRepository.GetAllAsync(cancellationToken);
            if (menus == null || !menus.Any())
            {
                return menus.Select(menu => new GetAllQueryResponse
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    Url = menu.Url,
                    DisplayOrder = menu.DisplayOrder,
                    ParentMenuId = menu.ParentMenuId
                }).ToList();
            }
            return menus.Select(menu => new GetAllQueryResponse
            {
                Id = menu.Id,
                Name = menu.Name,
                Url = menu.Url,
                DisplayOrder = menu.DisplayOrder,
                ParentMenuId = menu.ParentMenuId
            }).ToList();
        }
    }
}
