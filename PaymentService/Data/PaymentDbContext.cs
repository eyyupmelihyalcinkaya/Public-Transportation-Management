using Microsoft.EntityFrameworkCore;
using PaymentService.Entities;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }
    public DbSet<BoardingTransaction> BoardingTransactions { get; set; }
}