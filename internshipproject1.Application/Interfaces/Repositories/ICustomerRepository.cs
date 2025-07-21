using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        public Task<Customer> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken);
        public Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<bool> CustomerExistsAsync(int id, CancellationToken cancellationToken);
        public Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);
        public Task<IReadOnlyList<Customer>> GetAllActiveCustomersAsync(CancellationToken cancellationToken);
        public Task<IReadOnlyList<Customer>> GetAllInactiveCustomersAsync(CancellationToken cancellationToken);
        public Task<IReadOnlyList<Customer>> GetAllByIsStudentAsync(bool isStudent, CancellationToken cancellationToken);
        public Task<IReadOnlyList<Customer>> GetAllByNameAsync(string name, CancellationToken cancellationToken);
        public Task<IReadOnlyList<Customer>> GetAllBySurnameAsync(string surname, CancellationToken cancellationToken);

        Task<(IReadOnlyList<Customer> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
