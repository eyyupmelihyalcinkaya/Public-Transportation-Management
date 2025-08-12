using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.HasPermission
{
    public class HasPermissionQueryHandler : IRequestHandler<HasPermissionQueryRequest, HasPermissionQueryResponse>
    {
        private readonly IRoleMenuPermission _roleMenuPermissionRepository;
        public HasPermissionQueryHandler(IRoleMenuPermission roleMenuPermissionRepository)
        {
            _roleMenuPermissionRepository = roleMenuPermissionRepository;
        }
        public async Task<HasPermissionQueryResponse> Handle(HasPermissionQueryRequest request, CancellationToken cancellationToken)
        {
            var hasPermission = await _roleMenuPermissionRepository.HasPermissionAsync(request.UserId, request.MenuId, request.PermissionType, cancellationToken);
            return new HasPermissionQueryResponse { HasPermission = hasPermission };
        }
    }
}
