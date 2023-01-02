using Microsoft.EntityFrameworkCore;
using WatchList.Domain;
using WatchList.Infrastructure.Database;

namespace WatchList.Infrastructure;

public class Repository<T> : IRepository<T> 
    where T : Entity
{
    private readonly AppDbContext _db;

    protected DbSet<T> Set { get; }

    public Repository(AppDbContext db)
    {
        _db = db;
        Set = db.Set<T>();
    }

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _db.FindAsync<T>(id, cancellationToken).AsTask();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Set.AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Update(entity);
        return _db.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Remove(entity);
        return _db.SaveChangesAsync(cancellationToken);
    }
}