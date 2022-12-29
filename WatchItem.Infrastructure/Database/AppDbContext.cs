using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore;

namespace WatchItem.Infrastructure.Database;

public sealed class AppDbContext : DbContext
{
    public DbSet<WatchList.Domain.WatchItems.WatchItem> WatchItems { get; private set; } = null!;

    public AppDbContext(DbContextOptions options)
        : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        modelBuilder.ConfigureSmartEnum();
    }
}