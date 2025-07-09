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
    public class RouteRepository : IRouteRepository
    {
        private readonly AppDbContext _dbContext;
        public RouteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<myRoute> AddAsync(myRoute route)
        {
            await _dbContext.Route.AddAsync(route);
            await _dbContext.SaveChangesAsync();

            return route;
        }

        public async Task DeleteAsync(int id)
        {
            await _dbContext.Route.Where(r=> r.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(myRoute entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Route.Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<myRoute>> GetAllAsync()
        {   //burayı anlamadım, continue with the task kısmını
            var routes = await _dbContext.Route.ToListAsync();
            return routes;
        }

        public Task<myRoute> GetByIdAsync(int id)
        {
            var route = _dbContext.Route.FirstOrDefaultAsync(r => r.Id == id);

            return route;
        }

        public Task<myRoute> GetByRouteNameAsync(string routeName)
        {
            var route = _dbContext.Route.FirstOrDefaultAsync(r => r.Name == routeName);

            if(route == null)
            {
                throw new KeyNotFoundException($"Route with name {routeName} not found.");
            }
            return route;
        }
        public Task<bool> RouteExistsAsync(string routeName)
        {
            var route = _dbContext.Route.FirstOrDefaultAsync(r => r.Name == routeName);
            if(route == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public async Task<myRoute> UpdateAsync(myRoute route)
        {
            var routeToUpdate = await _dbContext.Route.FirstOrDefaultAsync(r => r.Id == route.Id);
            if (routeToUpdate == null)
            {
                throw new Exception($"Route with ID {route.Id} not found.");
            }
            routeToUpdate.Name = route.Name;
            routeToUpdate.Description = route.Description;
            routeToUpdate.StartLocation = route.StartLocation;
            routeToUpdate.EndLocation = route.EndLocation;

            await _dbContext.SaveChangesAsync();

            return routeToUpdate;
        }

        Task IGenericRepository<myRoute>.UpdateAsync(myRoute entity)
        {
            return UpdateAsync(entity);
        }
    }
}
