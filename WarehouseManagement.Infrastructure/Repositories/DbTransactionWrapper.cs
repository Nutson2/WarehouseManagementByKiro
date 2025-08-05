using Microsoft.EntityFrameworkCore.Storage;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Обертка для транзакции Entity Framework
/// </summary>
public class DbTransactionWrapper : IDbTransaction
{
    private readonly IDbContextTransaction _transaction;
    private bool _disposed = false;

    public DbTransactionWrapper(IDbContextTransaction transaction)
    {
        _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _transaction?.Dispose();
            _disposed = true;
        }
    }
}