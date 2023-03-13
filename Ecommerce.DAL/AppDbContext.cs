using Ecommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL;

public class AppDbContext : DbContext
{
    public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
    public DbSet<CategoryImage> CategoryImages { get; set; }
    public DbSet<CategoryAttributeName> CategoryAttributeNames { get; set; }
    public DbSet<CategoryDiscount> Discounts { get; set; }
    public DbSet<CategoryName> CategoryNames { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductAttributeName> ProductAttributeNames { get; set; }
    public DbSet<ProductDescription> ProductDescriptions { get; set; }
    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductTitle> ProductTitles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<ShippingMethod> ShippingMethods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    public AppDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<CategoryName>()
            .HasIndex(c => new { c.Name, c.Language })
            .IsUnique();

        modelBuilder
            .Entity<CategoryImage>()
            .HasKey(c => c.CategoryId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("PGSQL_STRING") + "Database=ecommerce";
        optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
    }
}