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
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<RoleMenuPermission> RoleMenuPermission { get; set; }



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

            modelBuilder.Entity<Role>(entity =>
            {
                entity
                .HasIndex(r=>r.Name)
                .IsUnique();
            });
            modelBuilder.Entity<UserRoles>(entity => {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                //Bir User'ın birden fazla Role'u olabilir
                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);
                //Bir Role'un birden fazla UserRole'ü olabilir
                entity.HasOne(ur=>ur.Role)
                    .WithMany(r=>r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<RoleMenuPermission>(entity => { 
                entity.HasKey(rmp => new { rmp.RoleId, rmp.MenuId });
                entity.HasOne(rmp => rmp.Role)
                    .WithMany(r => r.RoleMenuPermissions)
                    .HasForeignKey(rmp => rmp.RoleId);
                entity.HasOne(rmp => rmp.Menu)
                      .WithMany(m => m.RoleMenuPermissions)
                      .HasForeignKey(rmp => rmp.MenuId);

            });

            modelBuilder.Entity<Menu>(entity => {
                //Bir Menu'nun bir ParentMenu'sü olabilir
                entity.HasOne(m => m.ParentMenu)
                      .WithMany(m => m.SubMenus)
                      .HasForeignKey(m => m.ParentMenuId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Role>().HasData(

                new Role { Id = 1, Name = "SuperAdmin", Description = "God Mode"},
                new Role { Id = 2, Name = "Admin", Description = "Admin - Yönetici" },
                new Role {Id = 3, Name = "Passenger",Description = "End User - Yolcu" }
                );
            modelBuilder.Entity<Menu>().HasData(
                
                new Menu { Id = 1, Name = "Dashboard", Url = "/dashboard", ParentMenuId = null, DisplayOrder = 1 },
                new Menu { Id = 2, Name = "Users", Url = "/users", ParentMenuId = null, DisplayOrder = 2 },
                new Menu { Id = 3, Name = "Routes", Url = "/routes", ParentMenuId = null, DisplayOrder = 3 },
                new Menu { Id = 4, Name = "Stops", Url = "/stops", ParentMenuId = null, DisplayOrder = 4 },
                new Menu { Id = 5, Name = "Trips", Url = "/trips", ParentMenuId = null, DisplayOrder = 5 },
                new Menu { Id = 6, Name = "Cards", Url = "/cards", ParentMenuId = null, DisplayOrder = 6 },
                new Menu { Id = 7, Name = "Customers", Url = "/customers", ParentMenuId = null, DisplayOrder = 7 },
                new Menu { Id = 8, Name = "Settings", Url = "/settings", ParentMenuId = null, DisplayOrder = 8 },
                new Menu { Id = 9, Name = "Role Management", Url = "/role-management", ParentMenuId = null, DisplayOrder = 9 },
                new Menu { Id = 10, Name = "Card Transactions", Url = "/card-transactions", ParentMenuId = null, DisplayOrder = 10 }
                );

            modelBuilder.Entity<RoleMenuPermission>().HasData(
                new RoleMenuPermission { RoleId = 1, MenuId = 1 , CanCreate= true,CanDelete = true,CanRead = true,CanUpdate = true},
                new RoleMenuPermission { RoleId = 1, MenuId = 2, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 1, MenuId = 3, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 1, MenuId = 4, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 1, MenuId = 5, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 1, MenuId = 6, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 1, MenuId = 7, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 1, MenuId = 8, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 1, MenuId = 9, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 1, MenuId = 10, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },

                new RoleMenuPermission { RoleId = 2, MenuId = 1, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 2, MenuId = 2, CanCreate = false, CanDelete = false, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 2, MenuId = 3, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 2, MenuId = 4, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 2, MenuId = 5, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 2, MenuId = 6, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 2, MenuId = 7, CanCreate = false, CanDelete = false, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 2, MenuId = 8, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 2, MenuId = 9, CanCreate = false, CanDelete = false, CanRead = true, CanUpdate = false },
                new RoleMenuPermission { RoleId = 2, MenuId = 10, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },

                new RoleMenuPermission { RoleId = 3, MenuId = 1, CanCreate = false, CanDelete = false, CanRead = false, CanUpdate = false },
                new RoleMenuPermission { RoleId = 3, MenuId = 2, CanCreate = false, CanDelete = false, CanRead = false, CanUpdate = false },
                new RoleMenuPermission { RoleId = 3, MenuId = 3, CanCreate = false, CanDelete = false, CanRead = false, CanUpdate = false },
                new RoleMenuPermission { RoleId = 3, MenuId = 4, CanCreate = false, CanDelete = false, CanRead = false, CanUpdate = false },
                new RoleMenuPermission { RoleId = 3, MenuId = 5, CanCreate = false, CanDelete = false, CanRead = false, CanUpdate = false },
                new RoleMenuPermission { RoleId = 3, MenuId = 6, CanCreate = true, CanDelete = false, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 3, MenuId = 7, CanCreate = false, CanDelete = false, CanRead = false, CanUpdate = false },
                new RoleMenuPermission { RoleId = 3, MenuId = 8, CanCreate = false, CanDelete = false, CanRead = true, CanUpdate = true},
                new RoleMenuPermission { RoleId = 3, MenuId = 9, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true },
                new RoleMenuPermission { RoleId = 3, MenuId = 10, CanCreate = false, CanDelete = false, CanRead = true, CanUpdate = false }
                );

        }
            
    }

}
        


