using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Queries.IsExists
{
    public class IsExistsQueryHandler : IRequestHandler<IsExistsQueryRequest,IsExistsQueryResponse>
    {
        private readonly IMenuRepository _menuRepository;
        public IsExistsQueryHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<IsExistsQueryResponse> Handle(IsExistsQueryRequest request, CancellationToken cancellationToken)
        {
            var isExists = await _menuRepository.IsExists(request.Id, cancellationToken);
            return new IsExistsQueryResponse { IsExists = isExists };
        }
    }
}
