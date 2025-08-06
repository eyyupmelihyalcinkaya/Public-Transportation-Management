using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using internshipProject1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace internshipProject1.Infrastructure.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        
        public CustomerRepository(AppDbContext context)
        {
            // TODO: Melih class
            _context = context;
        }
        public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
            }
            await _context.Customer.AddAsync(customer,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return customer;
        }

        public async Task<bool> CustomerExistsAsync(int id, CancellationToken cancellationToken)
        {
            if(id<= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var exists = await _context.Customer.AnyAsync(c => c.Id == id, cancellationToken);
            return exists;
        }

        public Task<bool> CustomerExistsByUserId(int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
            }
            var customerExsts = _context.Customer.AnyAsync(c => c.UserId == userId && !c.IsDeleted, cancellationToken);
            return customerExsts;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var customer = await _context.Customer.FindAsync(id,cancellationToken);
            customer.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        public async Task DeleteAsync(Customer entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Customer cannot be null");
            }
            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return;

        }

        public async Task<IReadOnlyList<Customer>> GetAllActiveCustomersAsync(CancellationToken cancellationToken)
        {
            var activeCustomers = await _context.Customer
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
            return activeCustomers;
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            var customers = await _context.Customer
                .Where(c=> !c.IsDeleted)
                .ToListAsync(cancellationToken);
            return customers;
        }

        public async Task<IReadOnlyList<Customer>> GetAllByIsStudentAsync(bool isStudent, CancellationToken cancellationToken)
        {
             var students = await _context.Customer
                .Where(c=>c.IsStudent == isStudent && !c.IsDeleted)
                .ToListAsync(cancellationToken);
            return students;
        }

        public async Task<IReadOnlyList<Customer>> GetAllByNameAndSurnameAsync(string name, string surname, CancellationToken cancellationToken)
        {
            var customerByNameAndSurname = await _context.Customer
                .Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && !c.IsDeleted && c.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase))
                .ToListAsync(cancellationToken);
            return customerByNameAndSurname;
        }
        public async Task<IReadOnlyList<Customer>> GetAllInactiveCustomersAsync(CancellationToken cancellationToken)
        {
            var inactiveCustomers = await _context.Customer
                .Where(c => c.IsDeleted)
                .ToListAsync(cancellationToken);
            return inactiveCustomers;
        }

        public async Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var customerByEmail = await _context.Customer
                .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower() && !c.IsDeleted, cancellationToken);
            return customerByEmail;
        }


        public async Task<Customer> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var customerById = await _context.Customer
                .FirstOrDefaultAsync(c=>c.Id == id && !c.IsDeleted, cancellationToken);
            if (customerById == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found or has been deleted.");
            }
            return customerById;
        }

        public async Task<Customer> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            if(userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
            }
            var customerByUserId = await _context.Customer.FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted, cancellationToken);
            if (customerByUserId == null)
            {
                throw new KeyNotFoundException($"Customer with User ID {userId} not found or has been deleted.");
            }
            return customerByUserId;
        }

        public async Task<(IReadOnlyList<Customer> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var totalCount = await _context.Customer
                .Where(c => !c.IsDeleted)
                .CountAsync(cancellationToken);
            var items = await _context.Customer
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Id)
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return (items, totalCount);
        }

        public async Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            var existingCustomer = await _context.Customer.FindAsync(customer.Id, cancellationToken);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {customer.Id} not found or has been deleted.");
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.Surname = customer.Surname;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.IsStudent = customer.IsStudent;
            existingCustomer.DateOfBirth = customer.DateOfBirth;
            existingCustomer.IsDeleted = customer.IsDeleted;
            await _context.SaveChangesAsync(cancellationToken);
            return existingCustomer;
        }

        Task IGenericRepository<Customer>.UpdateAsync(Customer entity, CancellationToken cancellationToken)
        {
            return UpdateAsync(entity, cancellationToken);
        }
    }
}
