using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Queries.GetAllStudents
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQueryRequest, IReadOnlyList<GetAllStudentsQueryResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllStudentsQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IReadOnlyList<GetAllStudentsQueryResponse>> Handle(GetAllStudentsQueryRequest request, CancellationToken cancellationToken)
        { 
            var students = await _customerRepository.GetAllByIsStudentAsync(true, cancellationToken);
            var studentResponses = students.Select(student => new GetAllStudentsQueryResponse
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                IsStudent = student.IsStudent
            }).ToList();
            return studentResponses;
        }

    }
}
