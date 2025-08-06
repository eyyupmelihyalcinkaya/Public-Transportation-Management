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
        public DbSet<RouteToCreate> Route { get; set; }
        public DbSet<RouteStop> RouteStop { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<CardTransaction> CardTransaction { get; set; }
        public DbSet<Customer> Customer { get; set; }



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
            modelBuilder.Entity<RouteStop>()
                .HasIndex(rs => new { rs.RouteId, rs.StopId })
                .IsUnique();
            modelBuilder.Entity<RouteStop>()
                .HasIndex(rs => new { rs.RouteId, rs.Order})
                .IsUnique();
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.userName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.passwordHash).IsRequired();
                entity.Property(u => u.passwordSalt).IsRequired();
                entity.Property(u => u.Role).HasConversion<int>().IsRequired();
                entity.HasIndex(u => u.userName).IsUnique(); //unique username
            });
            modelBuilder.Entity<RouteToCreate>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Description).HasMaxLength(500);
                entity.Property(r => r.StartLocation).IsRequired().HasMaxLength(100);
                entity.Property(r => r.EndLocation).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.CreatedBy)
                  .WithMany(u => u.CreatedRoutes)
                  .HasForeignKey("CreatedById")
                  .IsRequired(false);

            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
                entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(15);
           /*     entity.HasOne(e => e.Card)
                    .WithOne(c => c.Customer)
                    .HasForeignKey<Card>(c => c.CustomerId);
           */
                entity.HasOne(e => e.User)
                    .WithOne(u => u.Customer)
                    .HasForeignKey<Customer>(c => c.UserId)
                    .IsRequired(false);

                entity.HasMany(e => e.CardList)
                .WithOne(e => e.Customer)
                .HasForeignKey(c => c.CustomerId);
            });
            modelBuilder.Entity<Card>(entity => { 
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Balance).HasColumnType("decimal(18,2)");
                entity.Property(c => c.ExpirationDate).IsRequired();
                entity.Property(c => c.IsActive).IsRequired();
                entity.HasMany(c => c.CardTransactions)
                    .WithOne(ct => ct.Card)
                    .HasForeignKey(ct => ct.CardId);
            });
            modelBuilder.Entity<CardTransaction>(entity => {
                entity.HasKey(ct => ct.Id);
                entity.Property(ct => ct.Amount).HasColumnType("decimal(18,2)");
                entity.Property(ct => ct.TransactionDate).IsRequired();
            });

            
        
        }
            
    }

}
        


