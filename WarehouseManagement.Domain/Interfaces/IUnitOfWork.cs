namespace WarehouseManagement.Domain.Interfaces;

/// <summary>
/// Интерфейс Unit of Work для управления транзакциями и репозиториями
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Репозиторий для работы с ресурсами
    /// </summary>
    IResourceRepository Resources { get; }

    /// <summary>
    /// Репозиторий для работы с единицами измерения
    /// </summary>
    IUnitOfMeasureRepository UnitsOfMeasure { get; }

    /// <summary>
    /// Репозиторий для работы с клиентами
    /// </summary>
    IClientRepository Clients { get; }

    /// <summary>
    /// Репозиторий для работы с балансом склада
    /// </summary>
    IBalanceRepository Balances { get; }

    /// <summary>
    /// Репозиторий для работы с документами поступления
    /// </summary>
    IReceiptDocumentRepository ReceiptDocuments { get; }

    /// <summary>
    /// Репозиторий для работы с документами отгрузки
    /// </summary>
    IShipmentDocumentRepository ShipmentDocuments { get; }

    /// <summary>
    /// Сохранить все изменения в базе данных
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество затронутых записей</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Начать транзакцию
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Объект транзакции</returns>
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Интерфейс для работы с транзакциями базы данных
/// </summary>
public interface IDbTransaction : IDisposable
{
    /// <summary>
    /// Подтвердить транзакцию
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Откатить транзакцию
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}