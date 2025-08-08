using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetAll
{
    public class GetAllUserRolesQueryHandler : IRequestHandler<GetAllUserRolesQueryRequest, List<GetAllUserRolesQueryResponse>>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public GetAllUserRolesQueryHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public async Task<List<GetAllUserRolesQueryResponse>> Handle(GetAllUserRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var userRoles = await _userRoleRepository.GetAllAsync(cancellationToken);
            if(userRoles == null || !userRoles.Any())
            {
                return new List<GetAllUserRolesQueryResponse>();
            }
            var response = userRoles.Select(ur => new GetAllUserRolesQueryResponse
            {
                UserId = ur.UserId,
                RoleId = ur.RoleId
            }).ToList();
            return response;
        }
    }
    
}
