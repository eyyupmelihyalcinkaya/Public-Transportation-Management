using internshipproject1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace internshipProject1.Infrastructure.Context

{   //
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Stop> Stop { get; set; }
        public DbSet<myRoute> Route { get; set; }
        public DbSet<RouteStop> RouteStop { get; set; }
        public DbSet<Trip> Trip { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Route)
                .WithMany(r => r.Trips)
                .HasForeignKey(t => t.RouteId);
            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.Route)
                .WithMany(r => r.RouteStops)
                .HasForeignKey(rs => rs.RouteId);
            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.Stop)
                .WithMany(s=>s.RouteStops)
                .HasForeignKey(rs => rs.StopId);
        }
            
    }

}
        


