using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Role.Queries.GetAll
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest,List<GetAllRolesQueryResponse>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<GetAllRolesQueryResponse>> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        { 
            var roles = await _roleRepository.GetAllAsync(cancellationToken);
            var response = roles.Select(role => new GetAllRolesQueryResponse
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            }).ToList();
            return response;
        }
    }
}
