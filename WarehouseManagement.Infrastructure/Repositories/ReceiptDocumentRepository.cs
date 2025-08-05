using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с документами поступления
/// </summary>
public class ReceiptDocumentRepository : Repository<ReceiptDocument>, IReceiptDocumentRepository
{
    public ReceiptDocumentRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByNumberAsync(string number, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(number))
            return false;

        var query = DbSet.Where(rd => rd.Number.ToLower() == number.ToLower());
        
        if (excludeId.HasValue)
        {
            query = query.Where(rd => rd.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<ReceiptDocument?> GetWithResourcesAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(rd => rd.Resources)
                .ThenInclude(rr => rr.Resource)
            .Include(rd => rd.Resources)
                .ThenInclude(rr => rr.UnitOfMeasure)
            .FirstOrDefaultAsync(rd => rd.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ReceiptDocument>> GetFilteredAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        IEnumerable<string>? numbers = null,
        IEnumerable<int>? resourceIds = null,
        IEnumerable<int>? unitIds = null,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet
            .Include(rd => rd.Resources)
                .ThenInclude(rr => rr.Resource)
            .Include(rd => rd.Resources)
                .ThenInclude(rr => rr.UnitOfMeasure)
            .AsQueryable();

        // Фильтрация по датам
        if (dateFrom.HasValue)
        {
            query = query.Where(rd => rd.Date >= dateFrom.Value.Date);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(rd => rd.Date <= dateTo.Value.Date.AddDays(1).AddTicks(-1));
        }

        // Фильтрация по номерам документов
        if (numbers != null)
        {
            var numbersList = numbers.Where(n => !string.IsNullOrWhiteSpace(n)).ToList();
            if (numbersList.Any())
            {
                query = query.Where(rd => numbersList.Contains(rd.Number));
            }
        }

        // Фильтрация по ресурсам (независимо от дат)
        if (resourceIds != null)
        {
            var resourceIdsList = resourceIds.ToList();
            if (resourceIdsList.Any())
            {
                query = query.Where(rd => rd.Resources.Any(rr => resourceIdsList.Contains(rr.ResourceId)));
            }
        }

        // Фильтрация по единицам измерения (независимо от дат)
        if (unitIds != null)
        {
            var unitIdsList = unitIds.ToList();
            if (unitIdsList.Any())
            {
                query = query.Where(rd => rd.Resources.Any(rr => unitIdsList.Contains(rr.UnitOfMeasureId)));
            }
        }

        return await query
            .OrderByDescending(rd => rd.Date)
            .ThenBy(rd => rd.Number)
            .ToListAsync(cancellationToken);
    }
}