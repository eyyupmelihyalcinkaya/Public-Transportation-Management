using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetRolesByUserId
{
    public class GetRolesByUserIdQueryHandler : IRequestHandler<GetRolesByUserIdQueryRequest, GetRolesByUserIdQueryResponse>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public GetRolesByUserIdQueryHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public async Task<GetRolesByUserIdQueryResponse> Handle(GetRolesByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var roles = await _userRoleRepository.GetRolesByUserIdAsync(request.UserId, cancellationToken);
            if (roles == null || !roles.Any())
            {
                return new GetRolesByUserIdQueryResponse
                {
                    UserId = request.UserId,
                    RoleIds = new List<int>()
                };
            }
            return new GetRolesByUserIdQueryResponse
            {
                UserId = request.UserId,
                RoleIds = roles.Select(role => role.Id).ToList()
            };

        }
    }
}
