using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Базовая реализация репозитория для работы с сущностями
/// </summary>
/// <typeparam name="T">Тип сущности, наследующей от BaseEntity</typeparam>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly WarehouseDbContext Context;
    protected readonly DbSet<T> DbSet;

    public Repository(WarehouseDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var result = await DbSet.AddAsync(entity, cancellationToken);
        return result.Entity;
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        await DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        DbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        DbSet.UpdateRange(entities);
    }

    public virtual void Remove(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        DbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        DbSet.RemoveRange(entities);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.CountAsync(predicate, cancellationToken);
    }
}