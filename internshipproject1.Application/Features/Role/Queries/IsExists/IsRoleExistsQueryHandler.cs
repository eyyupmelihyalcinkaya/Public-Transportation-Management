using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Role.Queries.IsExists
{
    public class IsRoleExistsQueryHandler : IRequestHandler<IsRoleExistsQueryRequest,IsRoleExistsQueryResponse>
    {
        private readonly IRoleRepository _roleRepository;
        public IsRoleExistsQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<IsRoleExistsQueryResponse> Handle(IsRoleExistsQueryRequest request, CancellationToken cancellationToken)
        {
            var isExists = await _roleRepository.IsExists(request.Id, cancellationToken);
            return new IsRoleExistsQueryResponse(isExists);
        }
    }
}
