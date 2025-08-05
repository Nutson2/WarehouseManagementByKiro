using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с балансом склада
/// </summary>
public class BalanceRepository : Repository<Balance>, IBalanceRepository
{
    public BalanceRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<Balance?> GetByResourceAndUnitAsync(int resourceId, int unitOfMeasureId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(b => b.Resource)
            .Include(b => b.UnitOfMeasure)
            .FirstOrDefaultAsync(b => b.ResourceId == resourceId && b.UnitOfMeasureId == unitOfMeasureId, cancellationToken);
    }

    public async Task<IEnumerable<Balance>> GetAllWithIncludesAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(b => b.Resource)
            .Include(b => b.UnitOfMeasure)
            .OrderBy(b => b.Resource.Name)
            .ThenBy(b => b.UnitOfMeasure.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Balance>> GetFilteredAsync(IEnumerable<int>? resourceIds = null, IEnumerable<int>? unitIds = null, CancellationToken cancellationToken = default)
    {
        var query = DbSet
            .Include(b => b.Resource)
            .Include(b => b.UnitOfMeasure)
            .AsQueryable();

        // Фильтрация по ресурсам
        if (resourceIds != null)
        {
            var resourceIdsList = resourceIds.ToList();
            if (resourceIdsList.Any())
            {
                query = query.Where(b => resourceIdsList.Contains(b.ResourceId));
            }
        }

        // Фильтрация по единицам измерения
        if (unitIds != null)
        {
            var unitIdsList = unitIds.ToList();
            if (unitIdsList.Any())
            {
                query = query.Where(b => unitIdsList.Contains(b.UnitOfMeasureId));
            }
        }

        return await query
            .OrderBy(b => b.Resource.Name)
            .ThenBy(b => b.UnitOfMeasure.Name)
            .ToListAsync(cancellationToken);
    }
}