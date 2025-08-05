using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с клиентами
/// </summary>
public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        var query = DbSet.Where(c => c.Name.ToLower() == name.ToLower());
        
        if (excludeId.HasValue)
        {
            query = query.Where(c => c.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<IEnumerable<Client>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(c => c.Status == EntityStatus.Active)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsUsedInDocumentsAsync(int clientId, CancellationToken cancellationToken = default)
    {
        // Проверяем использование в документах отгрузки
        return await Context.ShipmentDocuments
            .AnyAsync(sd => sd.ClientId == clientId, cancellationToken);
    }
}