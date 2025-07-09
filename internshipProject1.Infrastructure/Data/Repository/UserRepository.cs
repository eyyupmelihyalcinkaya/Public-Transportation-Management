using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using internshipProject1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace internshipProject1.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> AddAsync(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(r=>r.Id == id);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _dbContext.Users.Where(r => r.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

        }

        public Task DeleteAsync(User entity)
        {
            if(entitiy == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Users.Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public Task<User> GetByIdAsync(int id)
        {
            var user = _dbContext.Users.FirstOrDefaultAsync(r => r.Id == id);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user;
        }

        public Task<User> GetByUsernameAsync(string username)
        {
            var user = _dbContext.Users.FirstOrDefaultAsync(r => r.userName == username);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user;
        }

        public Task<User> UpdateAsync(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefaultAsync(r => r.Id == user.Id);
            if (existingUser == null)
            {
                throw new ArgumentNullException(nameof(existingUser));
            }
            _dbContext.Users.Update(user);
            return _dbContext.SaveChangesAsync().ContinueWith(_ => user);
        }

        public Task<bool> UserExistsAsync(string username)
        {
            var existingUser = _dbContext.Users.AnyAsync(r => r.userName == username);
            return existingUser;
        }

        Task<IReadOnlyList<User>> IGenericRepository<User>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<User>.UpdateAsync(User entity)
        {
            return UpdateAsync(entity);
        }
    }
}
