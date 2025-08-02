using internshipproject1.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipProject1.Infrastructure.Context;
using internshipproject1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using internshipproject1.Application.DTOs;


namespace internshipProject1.Infrastructure.Data.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly AppDbContext _dbContext;

        public RouteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RouteToCreate> AddAsync(RouteToCreate route, CancellationToken cancellationToken)
        {
            await _dbContext.Route.AddAsync(route, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return route;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _dbContext.Route.Where(r => r.Id == id).ExecuteDeleteAsync(cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteAsync(RouteToCreate entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Route.Remove(entity);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<RouteToCreate>> GetAllAsync()
        {
            var routes = await _dbContext.Route.ToListAsync();
            return routes;
        }

        public async Task<IReadOnlyList<RouteToCreate>> GetAllAsync(CancellationToken cancellationToken)
        {
            var route = await _dbContext.Route.ToListAsync(cancellationToken);
            return route;
        }

        public Task<RouteToCreate> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var route = _dbContext.Route.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

            return route;
        }

        public Task<RouteToCreate> GetByRouteNameAsync(string routeName, CancellationToken cancellationToken)
        {
            var route = _dbContext.Route.FirstOrDefaultAsync(r => r.Name == routeName, cancellationToken);

            if (route == null)
            {
                throw new KeyNotFoundException($"Route with name {routeName} not found.");
            }
            return route;
        }
        public Task<bool> RouteExistsAsync(string routeName, CancellationToken cancellationToken)
        {
            var route = _dbContext.Route.FirstOrDefaultAsync(r => r.Name == routeName, cancellationToken);
            if (route == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public async Task<RouteToCreate> UpdateAsync(RouteToCreate route, CancellationToken cancellationToken)
        {
            var routeToUpdate = await _dbContext.Route.FirstOrDefaultAsync(r => r.Id == route.Id, cancellationToken);
            if (routeToUpdate == null)
            {
                throw new Exception($"Route with ID {route.Id} not found.");
            }
            routeToUpdate.Name = route.Name;
            routeToUpdate.Description = route.Description;
            routeToUpdate.StartLocation = route.StartLocation;
            routeToUpdate.EndLocation = route.EndLocation;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return routeToUpdate;
        }

        Task IGenericRepository<RouteToCreate>.UpdateAsync(RouteToCreate entity, CancellationToken cancellationToken)
        {
            return UpdateAsync(entity, cancellationToken);
        }
        public async Task<bool> RouteExistByIdAsync(int routeId, CancellationToken cancellationToken)
        {
            var route = await _dbContext.Route.AnyAsync(r => r.Id == routeId, cancellationToken);
            return route;
        }
        public async Task<int> TotalRoutesCount(CancellationToken cancellationToken)
        {
            return await _dbContext.Route.CountAsync(cancellationToken);
        }

        public async Task<RouteToCreate> CreateRouteWithStops(RouteToCreate route, List<StopCreateDTO> stops, CancellationToken cancellationToken)
        {
            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            if (stops == null || !stops.Any())
            {
                throw new ArgumentException("At least one stop is required.", nameof(stops));
            }

            // 1. Rotayı ekle
            await _dbContext.Route.AddAsync(route, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken); // ID oluşsun

            // 2. Her durağı veritabanına ekle ve ID'yi al
            foreach (var stopDto in stops)
            {
                var stop = new Stop
                {
                    Name = stopDto.StopName,
                    Latitude = stopDto.Latitude,
                    Longitude = stopDto.Longitude
                };

                await _dbContext.Stop.AddAsync(stop, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken); // ID oluşsun

                // 3. Ara tabloya ekle
                var routeStop = new RouteStop
                {
                    RouteId = route.Id,
                    StopId = stop.Id,
                    Order = stopDto.Order
                };

                await _dbContext.RouteStop.AddAsync(routeStop, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return route;
        }

    }
}
