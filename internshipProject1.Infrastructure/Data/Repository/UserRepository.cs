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
        public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(r=>r.Id == id,cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _dbContext.Users.Where(r => r.Id == id).ExecuteDeleteAsync(cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        public Task DeleteAsync(User entity, CancellationToken cancellationToken)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Users.Remove(entity);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.ToListAsync(cancellationToken);
            return users;
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user;
        }

        public async Task<User> GetByUsernameAsync(string username,CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(r => r.userName == username,cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user;
        }

        public  Task<User> UpdateAsync(User user,CancellationToken cancellationToken)
        {
            var existingUser =  _dbContext.Users.FirstOrDefaultAsync(r => r.Id == user.Id,cancellationToken);
            if (existingUser == null)
            {
                throw new ArgumentNullException(nameof(existingUser));
            }
            _dbContext.Users.Update(user);
            return _dbContext.SaveChangesAsync(cancellationToken).ContinueWith(_ => user);
        }

        public Task<bool> UserExistsAsync(string username, CancellationToken cancellationToken)
        {
            var existingUser = _dbContext.Users.AnyAsync(r => r.userName == username,cancellationToken);
            return existingUser;
        }

        async Task<IReadOnlyList<User>> IGenericRepository<User>.GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.ToListAsync(cancellationToken);
            return users.AsReadOnly();
        }

        Task IGenericRepository<User>.UpdateAsync(User entity, CancellationToken cancellationToken)
        {
            return UpdateAsync(entity,cancellationToken);
        }
    }
}
