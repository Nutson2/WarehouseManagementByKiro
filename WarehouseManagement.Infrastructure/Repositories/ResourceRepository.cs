using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с ресурсами
/// </summary>
public class ResourceRepository : Repository<Resource>, IResourceRepository
{
    public ResourceRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        var query = DbSet.Where(r => r.Name.ToLower() == name.ToLower());
        
        if (excludeId.HasValue)
        {
            query = query.Where(r => r.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<IEnumerable<Resource>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(r => r.Status == EntityStatus.Active)
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsUsedInDocumentsAsync(int resourceId, CancellationToken cancellationToken = default)
    {
        // Проверяем использование в документах поступления
        var usedInReceipts = await Context.ReceiptResources
            .AnyAsync(rr => rr.ResourceId == resourceId, cancellationToken);

        if (usedInReceipts)
            return true;

        // Проверяем использование в документах отгрузки
        var usedInShipments = await Context.ShipmentResources
            .AnyAsync(sr => sr.ResourceId == resourceId, cancellationToken);

        if (usedInShipments)
            return true;

        // Проверяем использование в балансе
        var usedInBalance = await Context.Balances
            .AnyAsync(b => b.ResourceId == resourceId, cancellationToken);

        return usedInBalance;
    }
}