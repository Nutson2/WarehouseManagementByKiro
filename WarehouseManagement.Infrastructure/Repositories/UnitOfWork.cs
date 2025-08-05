using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories;

/// <summary>
/// Реализация Unit of Work для управления транзакциями и репозиториями
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseDbContext _context;
    private bool _disposed = false;

    // Lazy-loaded repositories
    private IResourceRepository? _resources;
    private IUnitOfMeasureRepository? _unitsOfMeasure;
    private IClientRepository? _clients;
    private IBalanceRepository? _balances;
    private IReceiptDocumentRepository? _receiptDocuments;
    private IShipmentDocumentRepository? _shipmentDocuments;

    public UnitOfWork(WarehouseDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IResourceRepository Resources
    {
        get
        {
            _resources ??= new ResourceRepository(_context);
            return _resources;
        }
    }

    public IUnitOfMeasureRepository UnitsOfMeasure
    {
        get
        {
            _unitsOfMeasure ??= new UnitOfMeasureRepository(_context);
            return _unitsOfMeasure;
        }
    }

    public IClientRepository Clients
    {
        get
        {
            _clients ??= new ClientRepository(_context);
            return _clients;
        }
    }

    public IBalanceRepository Balances
    {
        get
        {
            _balances ??= new BalanceRepository(_context);
            return _balances;
        }
    }

    public IReceiptDocumentRepository ReceiptDocuments
    {
        get
        {
            _receiptDocuments ??= new ReceiptDocumentRepository(_context);
            return _receiptDocuments;
        }
    }

    public IShipmentDocumentRepository ShipmentDocuments
    {
        get
        {
            _shipmentDocuments ??= new ShipmentDocumentRepository(_context);
            return _shipmentDocuments;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return new DbTransactionWrapper(transaction);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _context?.Dispose();
            _disposed = true;
        }
    }
}