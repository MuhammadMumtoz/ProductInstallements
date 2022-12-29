using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infrastructure.Context;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Installement> Installements { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
        .HasKey(bc => new { bc.InstallementId, bc.CustomerId });
        modelBuilder.Entity<Order>()
        .HasOne(bc => bc.Installement)
        .WithMany(b => b.Orders)
        .HasForeignKey(bc => bc.InstallementId);
        modelBuilder.Entity<Order>()
        .HasOne(bc => bc.Customer)
        .WithMany(c => c.Orders)
        .HasForeignKey(bc => bc.CustomerId);
    }
}