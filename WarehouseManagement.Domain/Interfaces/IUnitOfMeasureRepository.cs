using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с единицами измерения
/// </summary>
public interface IUnitOfMeasureRepository : IRepository<UnitOfMeasure>
{
    /// <summary>
    /// Проверить существование единицы измерения с указанным наименованием
    /// </summary>
    /// <param name="name">Наименование единицы измерения</param>
    /// <param name="excludeId">Идентификатор единицы измерения для исключения из проверки (для редактирования)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True, если единица измерения с таким наименованием существует</returns>
    Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все активные единицы измерения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция активных единиц измерения</returns>
    Task<IEnumerable<UnitOfMeasure>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить, используется ли единица измерения в документах
    /// </summary>
    /// <param name="unitId">Идентификатор единицы измерения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True, если единица измерения используется</returns>
    Task<bool> IsUsedInDocumentsAsync(int unitId, CancellationToken cancellationToken = default);
}