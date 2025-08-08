using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetUserRole
{
    public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQueryRequest, GetUserRoleQueryResponse>
    {
         private readonly IUserRoleRepository _userRoleRepository;
         public GetUserRoleQueryHandler(IUserRoleRepository userRoleRepository)
         {
             _userRoleRepository = userRoleRepository;
         }
        public async Task<GetUserRoleQueryResponse> Handle(GetUserRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var userRole = await _userRoleRepository.GetUserRoleAsync(request.UserId, request.RoleId, cancellationToken);
            if (userRole == null)
            {
                throw new Exception($"User with ID {request.UserId} does not have role with ID {request.RoleId}.");
            }
            var result = new GetUserRoleQueryResponse
            {
                UserId = userRole.UserId,
                UserName = userRole.User.userName,
                RoleId = userRole.RoleId,
                RoleName = userRole.Role.Name
            };
            return result;

        }
    }
    
}
