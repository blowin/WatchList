﻿namespace WatchList.Domain;

public interface IRepository<T> : IScoped
    where T : Entity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}