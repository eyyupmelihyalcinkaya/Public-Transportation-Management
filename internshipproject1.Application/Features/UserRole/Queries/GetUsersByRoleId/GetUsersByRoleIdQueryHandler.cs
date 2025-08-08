using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetUsersByRoleId
{
    public class GetUsersByRoleIdQueryHandler : IRequestHandler<GetUsersByRoleIdQueryRequest,List<GetUsersByRoleIdQueryResponse>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public GetUsersByRoleIdQueryHandler(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<List<GetUsersByRoleIdQueryResponse>> Handle(GetUsersByRoleIdQueryRequest request, CancellationToken cancellationToken)
        { 
            var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
            if (role == null)
            {
                return new List<GetUsersByRoleIdQueryResponse>();
            }
            var users = await _userRoleRepository.GetUsersByRoleIdAsync(request.RoleId, cancellationToken);
            if (users == null || !users.Any())
            {
                return new List<GetUsersByRoleIdQueryResponse>();
            }
            var response = users.Select(user => new GetUsersByRoleIdQueryResponse
            {
                UserId = user.Id,
                UserName = user.userName,
                Email = user.Customer.Email ?? string.Empty,
                RoleName = role.Name
            }).ToList();
            return response;
        }
    }
}
