using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с балансом склада
/// </summary>
public interface IBalanceRepository : IRepository<Balance>
{
    /// <summary>
    /// Получить баланс по ресурсу и единице измерения
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <param name="unitOfMeasureId">Идентификатор единицы измерения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Баланс или null, если не найден</returns>
    Task<Balance?> GetByResourceAndUnitAsync(int resourceId, int unitOfMeasureId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все балансы с включенными связанными сущностями
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция балансов с ресурсами и единицами измерения</returns>
    Task<IEnumerable<Balance>> GetAllWithIncludesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить отфильтрованные балансы
    /// </summary>
    /// <param name="resourceIds">Идентификаторы ресурсов для фильтрации (null - все ресурсы)</param>
    /// <param name="unitIds">Идентификаторы единиц измерения для фильтрации (null - все единицы)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция отфильтрованных балансов</returns>
    Task<IEnumerable<Balance>> GetFilteredAsync(IEnumerable<int>? resourceIds = null, IEnumerable<int>? unitIds = null, CancellationToken cancellationToken = default);
}