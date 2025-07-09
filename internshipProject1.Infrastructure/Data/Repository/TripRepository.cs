using internshipProject1.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace internshipProject1.Infrastructure.Data.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext _dbContext;

        public TripRepository(AppDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public async Task<Trip> GetByIdAsync(int id) { 
            if(id <= 0)
            {
                throw new ArgumentException("ID must be greater than zero.", nameof(id));
            }
            var trip = await _dbContext.Trip.FirstOrDefaultAsync(t => t.Id == id);
            if (trip == null)
            {
                throw new KeyNotFoundException($"Trip with ID {id} not found.");
            }
            return trip;
        }

        public async Task<Trip> GetByRouteIdAsync(int routeId) { 
            if(routeId <= 0)
            {
                throw new ArgumentException("Route ID must be greater than zero.", nameof(routeId));
            }
            var trip = await _dbContext.Trip.FirstOrDefaultAsync(t => t.RouteId == routeId);
            if (trip == null)
            {
                throw new KeyNotFoundException($"Trip with Route ID {routeId} not found.");
            }
            return trip;
        }

        public async Task<IReadOnlyList<Trip>> GetAllAsync() { 
            var trip = await _dbContext.Trip.ToListAsync();
            if (trip == null || !trip.Any())
            {
                throw new KeyNotFoundException("No trips found.");
            }
            return trip;
        }

        public async Task<Trip> AddAsync(Trip trip) { 
            if(trip == null)
            {
                throw new ArgumentNullException(nameof(trip), "Trip cannot be null.");
            }
            await _dbContext.Trip.AddAsync(trip);
            await _dbContext.SaveChangesAsync();
            return trip;
        }
        public async Task<Trip> UpdateAsync(Trip trip) {
            if (trip == null) { 
                throw new ArgumentNullException(nameof(trip), "Trip cannot be null.");
            }
            var updatedTrip = await _dbContext.Trip.FirstOrDefaultAsync(t => t.Id == trip.Id);

            updatedTrip.RouteId = trip.RouteId;
            updatedTrip.StartTime = trip.StartTime;
            updatedTrip.EndTime = trip.EndTime;
            updatedTrip.DayType = trip.DayType;

            await _dbContext.SaveChangesAsync();
            return updatedTrip;
        }
        public async Task<Trip> DeleteAsync(int id) { 
            if(id <= 0)
            {
                throw new ArgumentException("ID must be greater than zero.", nameof(id));
            }
            var trip = await _dbContext.Trip.FirstOrDefaultAsync(t => t.Id == id);
            if(trip == null)
            {
                throw new KeyNotFoundException($"Trip with ID {id} not found.");
            }
            _dbContext.Trip.Remove(trip);
            await _dbContext.SaveChangesAsync();

            return trip;
        }

        Task IGenericRepository<Trip>.UpdateAsync(Trip entity)
        {
            return UpdateAsync(entity);
        }

        public Task DeleteAsync(Trip entity)
        {
            return DeleteAsync(entity.Id);
        }
    }
}
