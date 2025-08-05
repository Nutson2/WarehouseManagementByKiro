using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с документами отгрузки
/// </summary>
public class ShipmentDocumentRepository : Repository<ShipmentDocument>, IShipmentDocumentRepository
{
    public ShipmentDocumentRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByNumberAsync(string number, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(number))
            return false;

        var query = DbSet.Where(sd => sd.Number.ToLower() == number.ToLower());
        
        if (excludeId.HasValue)
        {
            query = query.Where(sd => sd.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<ShipmentDocument?> GetWithResourcesAndClientAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(sd => sd.Client)
            .Include(sd => sd.Resources)
                .ThenInclude(sr => sr.Resource)
            .Include(sd => sd.Resources)
                .ThenInclude(sr => sr.UnitOfMeasure)
            .FirstOrDefaultAsync(sd => sd.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ShipmentDocument>> GetFilteredAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        IEnumerable<string>? numbers = null,
        IEnumerable<int>? resourceIds = null,
        IEnumerable<int>? unitIds = null,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet
            .Include(sd => sd.Client)
            .Include(sd => sd.Resources)
                .ThenInclude(sr => sr.Resource)
            .Include(sd => sd.Resources)
                .ThenInclude(sr => sr.UnitOfMeasure)
            .AsQueryable();

        // Фильтрация по датам
        if (dateFrom.HasValue)
        {
            query = query.Where(sd => sd.Date >= dateFrom.Value.Date);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(sd => sd.Date <= dateTo.Value.Date.AddDays(1).AddTicks(-1));
        }

        // Фильтрация по номерам документов
        if (numbers != null)
        {
            var numbersList = numbers.Where(n => !string.IsNullOrWhiteSpace(n)).ToList();
            if (numbersList.Any())
            {
                query = query.Where(sd => numbersList.Contains(sd.Number));
            }
        }

        // Фильтрация по ресурсам (независимо от дат)
        if (resourceIds != null)
        {
            var resourceIdsList = resourceIds.ToList();
            if (resourceIdsList.Any())
            {
                query = query.Where(sd => sd.Resources.Any(sr => resourceIdsList.Contains(sr.ResourceId)));
            }
        }

        // Фильтрация по единицам измерения (независимо от дат)
        if (unitIds != null)
        {
            var unitIdsList = unitIds.ToList();
            if (unitIdsList.Any())
            {
                query = query.Where(sd => sd.Resources.Any(sr => unitIdsList.Contains(sr.UnitOfMeasureId)));
            }
        }

        return await query
            .OrderByDescending(sd => sd.Date)
            .ThenBy(sd => sd.Number)
            .ToListAsync(cancellationToken);
    }
}