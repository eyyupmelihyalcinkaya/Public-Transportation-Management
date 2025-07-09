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
    public class StopRepository : IStopRepository
    {
        private readonly AppDbContext _dbContext;

        public StopRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Stop> GetByIdAsync(int id)
        {
            var stop = await _dbContext.Stop.FirstOrDefaultAsync(s => s.Id == id);
            if (stop == null)
            {
                throw new KeyNotFoundException($"Stop with ID {id} not found.");
            }
            return stop;
        }
        public async Task<Stop> GetByStopNameAsync(string stopName)
        {
            var stop = await _dbContext.Stop.FirstOrDefaultAsync(s => s.Name == stopName);
            if (stop == null)
            {
                throw new Exception("Stop cannot found");
            }
            return stop;
        }

        public async Task<Stop> AddAsync(Stop stop)
        {
            if (stop == null)
            {
                throw new ArgumentNullException(nameof(stop));
            }
            await _dbContext.Stop.AddAsync(stop);
            await _dbContext.SaveChangesAsync();
            return stop;
        }

        public async Task<Stop> UpdateAsync(Stop stop)
        {
            if (stop == null)
            {
                throw new ArgumentNullException(nameof(stop));
            }
            var updatedStop = await _dbContext.Stop.FirstOrDefaultAsync(s => s.Id == stop.Id);

            if (updatedStop == null)
            {
                throw new KeyNotFoundException($"Stop with ID {stop.Id} not found.");
            }
            updatedStop.Name = stop.Name;
            updatedStop.Latitude = stop.Latitude;
            updatedStop.Longitude = stop.Longitude;

            _dbContext.Stop.Update(updatedStop);
            await _dbContext.SaveChangesAsync();
            return updatedStop;

        }

        public async Task DeleteAsync(int id)
        {

            var stop = await _dbContext.Stop.FirstOrDefaultAsync(s => s.Id == id);
            if (stop == null)
            {
                throw new KeyNotFoundException($"Stop with ID {id} not found.");
            }
            _dbContext.Stop.Remove(stop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> StopExistsAsync(string stopName)
        {
            if (string.IsNullOrEmpty(stopName))
            {
                throw new ArgumentNullException(nameof(stopName));
            }
            return await _dbContext.Stop.AnyAsync(s => s.Name == stopName);
        }

        public async Task<IReadOnlyList<Stop>> GetAllAsync()
        {
            var stops = await _dbContext.Stop.ToListAsync();
            return stops;
        }

        Task IGenericRepository<Stop>.UpdateAsync(Stop entitiy)
        {
            return UpdateAsync(entitiy);
        }

        public Task DeleteAsync(Stop entitiy)
        {
            return DeleteAsync(entitiy.Id);
        }
    }
}
