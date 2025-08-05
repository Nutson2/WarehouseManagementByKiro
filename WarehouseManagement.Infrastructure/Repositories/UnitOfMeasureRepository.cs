using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с единицами измерения
/// </summary>
public class UnitOfMeasureRepository : Repository<UnitOfMeasure>, IUnitOfMeasureRepository
{
    public UnitOfMeasureRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        var query = DbSet.Where(u => u.Name.ToLower() == name.ToLower());
        
        if (excludeId.HasValue)
        {
            query = query.Where(u => u.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<IEnumerable<UnitOfMeasure>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(u => u.Status == EntityStatus.Active)
            .OrderBy(u => u.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsUsedInDocumentsAsync(int unitId, CancellationToken cancellationToken = default)
    {
        // Проверяем использование в документах поступления
        var usedInReceipts = await Context.ReceiptResources
            .AnyAsync(rr => rr.UnitOfMeasureId == unitId, cancellationToken);

        if (usedInReceipts)
            return true;

        // Проверяем использование в документах отгрузки
        var usedInShipments = await Context.ShipmentResources
            .AnyAsync(sr => sr.UnitOfMeasureId == unitId, cancellationToken);

        if (usedInShipments)
            return true;

        // Проверяем использование в балансе
        var usedInBalance = await Context.Balances
            .AnyAsync(b => b.UnitOfMeasureId == unitId, cancellationToken);

        return usedInBalance;
    }
}