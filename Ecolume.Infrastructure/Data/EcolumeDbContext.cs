using Ecolume.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecolume.Infrastructure.Data;
public class EcolumeDbContext : DbContext
{
    public EcolumeDbContext(DbContextOptions<EcolumeDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).IsRequired().HasMaxLength(500);

            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId);

            entity.HasOne(p => p.Brand)
                  .WithMany(b => b.Products)
                  .HasForeignKey(p => p.BrandId);

            entity.HasMany(p => p.Images)
                  .WithOne(i => i.Product)
                  .HasForeignKey(i => i.ProductId);

            entity.HasMany(p => p.Reviews)
                  .WithOne(r => r.Product)
                  .HasForeignKey(r => r.ProductId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Url).IsRequired();
            entity.HasOne(i => i.Product)
                  .WithMany(p => p.Images)
                  .HasForeignKey(i => i.ProductId);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.UserName).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Comment).HasMaxLength(1000);
            entity.Property(r => r.Rating).IsRequired();

            entity.HasOne(r => r.Product)
                  .WithMany(p => p.Reviews)
                  .HasForeignKey(r => r.ProductId);
        });
    }
}

