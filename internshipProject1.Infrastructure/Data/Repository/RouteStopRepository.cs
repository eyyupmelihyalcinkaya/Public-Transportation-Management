using internshipproject1.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipProject1.Infrastructure.Context;
using internshipproject1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace internshipProject1.Infrastructure.Data.Repository
{
    public class RouteStopRepository : IRouteStopRepository
    {

        private readonly AppDbContext _dbContext;

        public RouteStopRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<RouteStop> AddAsync(RouteStop routeStop, CancellationToken cancellationToken)
        {
            if(routeStop == null)
            {
                throw new ArgumentNullException(nameof(routeStop));
            }
            await _dbContext.RouteStop.AddAsync(routeStop);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return routeStop;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid route stop ID", nameof(id));
            }
            var routeStop = await _dbContext.RouteStop.FindAsync(id);
            _dbContext.RouteStop.Remove(routeStop);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteAsync(RouteStop entity, CancellationToken cancellationToken)
        {
             if(entity == null)
             {
                throw new ArgumentNullException(nameof(entity));
             }
             _dbContext.RouteStop.Remove(entity);
             return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<RouteStop>> GetAllAsync(CancellationToken cancellationToken)
        {
            var routeStops = await _dbContext.RouteStop.ToListAsync(cancellationToken);
            return routeStops;
        }

        public async Task<IReadOnlyList<RouteStop>> GetAllByRouteIdAsync(int routeId, CancellationToken cancellationToken)
        {
            if(routeId <= 0)
            {
                throw new ArgumentException("Invalid route ID", nameof(routeId));
            }
            var routeStop = await _dbContext.RouteStop.Where(r=> r.RouteId == routeId).ToListAsync(cancellationToken);
            return routeStop;
        }

        public async Task<IReadOnlyList<RouteStop>> GetAllByStopIdAsync(int stopId,CancellationToken cancellationToken)
        {
            if (stopId <= 0)
            {
                throw new ArgumentException("Invalid route ID", nameof(stopId));
            }
            var routeStop = await _dbContext.RouteStop.Where(r => r.StopId == stopId).ToListAsync(cancellationToken);
            return routeStop;

        }

        public async Task<RouteStop> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid route stop ID", nameof(id));
            }
            var routeStop = await _dbContext.RouteStop.FirstOrDefaultAsync(rs => rs.Id == id, cancellationToken);
            if(routeStop == null)
            {
                throw new KeyNotFoundException($"RouteStop with ID {id} not found.");
            }
            return routeStop;
        }

        public async Task<RouteStop> GetByRouteIdAndStopIdAsync(int routeId, int stopId,CancellationToken cancellationToken)
        {
            if (routeId <= 0 || stopId <= 0) { 
                throw new ArgumentException("Invalid route or stop ID");
            }
            var routeStop = await _dbContext.RouteStop.Where(rs => rs.RouteId == routeId && rs.StopId == stopId).FirstOrDefaultAsync(cancellationToken);
            if (routeStop == null)
            {
                throw new KeyNotFoundException($"RouteStop with RouteId {routeId} and StopId {stopId} not found.");
            }
            return routeStop;
        }

        public async Task<bool> RouteStopExistsAsync(int routeId, int stopId, CancellationToken cancellationToken)
        {
            if(routeId <= 0 || stopId <= 0)
            {
                throw new ArgumentException("Invalid route or stop ID");
            }
            return await _dbContext.RouteStop.AnyAsync(rs => rs.RouteId == routeId && rs.StopId == stopId, cancellationToken);

        }

        public Task<RouteStop> UpdateAsync(RouteStop routeStop,CancellationToken cancellationToken)
        {
            return UpdateAsync(routeStop,cancellationToken);
        }

        Task IGenericRepository<RouteStop>.UpdateAsync(RouteStop entity, CancellationToken cancellationToken)
        {
            return UpdateAsync(entity, cancellationToken);
        }
    }
}
