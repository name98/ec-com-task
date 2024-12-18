using EdCom.Domain;
using Microsoft.EntityFrameworkCore;

namespace EdCom.Data;

public class EdComDbContext : DbContext
{
    public EdComDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Purchase> Purchases { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EdComDbContext).Assembly);
    }
}