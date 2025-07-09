using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.User.Queries.getAllUsers
{
    public class getAllUsersQueryHandler : IRequestHandler<getAllUsersQueryRequest, List<getAllUsersQueryResponse>>
    {
        private readonly IUserRepository _userRepository;

        public getAllUsersQueryHandler(IUserRepository userRepository)
        { 
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<List<getAllUsersQueryResponse>> Handle(getAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var response = users.Select(u => new getAllUsersQueryResponse
            {
                id = u.Id,
                userName = u.userName
            }).ToList();

            return response;
        }

    }
}
