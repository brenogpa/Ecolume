using Ecolume.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecolume.Infrastructure.Data;

public class EcolumeDbContext : DbContext
{
    public EcolumeDbContext(DbContextOptions<EcolumeDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
}