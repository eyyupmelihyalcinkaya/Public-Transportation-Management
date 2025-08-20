using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.IsUserInRole
{
    public class IsUserInRoleQueryHandler : IRequestHandler<IsUserInRoleQueryRequest,IsUserInRoleQueryResponse>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public IsUserInRoleQueryHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IsUserInRoleQueryResponse> Handle(IsUserInRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var isInRole = await _userRoleRepository.IsUserInRoleAsync(request.UserId, request.RoleId, cancellationToken);
            return new IsUserInRoleQueryResponse
            {
                IsInRole = isInRole
            };
        }
    }
}
