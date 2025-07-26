using Infrastructure.Cache;
using internshipproject1.Application.Interfaces;
using internshipproject1.Application.Interfaces.Services;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Services;
using internshipProject1.Infrastructure.Context;
using internshipProject1.Infrastructure.Data.Repository;
using internshipProject1.Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using internshipProject1.Infrastructure.Settings;
namespace internshipProject1.Infrastructure
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // MongoDB Configuration
            var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            if (mongoDbSettings != null)
            {
                services.AddSingleton(mongoDbSettings);
                services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoDbSettings.ConnectionString));
                services.AddSingleton(sp =>
                {
                    var client = sp.GetRequiredService<IMongoClient>();
                    return client.GetDatabase(mongoDbSettings.DatabaseName);
                });
                services.AddScoped<MongoDbContext>();
            }

            // Repository'ler
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IStopRepository, StopRepository>();
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<IRouteStopRepository, RouteStopRepository>();
            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
            services.AddScoped<IErrorLogService, ErrorLogService>();
            
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICardTransaction, CardTransactionRepository>();
            // Cache Service
            services.AddScoped<IRedisCacheService, RedisCacheService>();

            // Password Hashing Service
            services.AddScoped<IPasswordHashingService, PasswordHashingService>();

            return services;
        }
    }
}