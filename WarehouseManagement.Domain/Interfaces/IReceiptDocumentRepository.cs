using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с документами поступления
/// </summary>
public interface IReceiptDocumentRepository : IRepository<ReceiptDocument>
{
    /// <summary>
    /// Проверить существование документа поступления с указанным номером
    /// </summary>
    /// <param name="number">Номер документа</param>
    /// <param name="excludeId">Идентификатор документа для исключения из проверки (для редактирования)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True, если документ с таким номером существует</returns>
    Task<bool> ExistsByNumberAsync(string number, int? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить документ поступления с ресурсами
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Документ поступления с ресурсами или null</returns>
    Task<ReceiptDocument?> GetWithResourcesAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить отфильтрованные документы поступления
    /// </summary>
    /// <param name="dateFrom">Дата начала периода (включительно)</param>
    /// <param name="dateTo">Дата окончания периода (включительно)</param>
    /// <param name="numbers">Номера документов для фильтрации (null - все номера)</param>
    /// <param name="resourceIds">Идентификаторы ресурсов для фильтрации (null - все ресурсы)</param>
    /// <param name="unitIds">Идентификаторы единиц измерения для фильтрации (null - все единицы)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция отфильтрованных документов поступления</returns>
    Task<IEnumerable<ReceiptDocument>> GetFilteredAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        IEnumerable<string>? numbers = null,
        IEnumerable<int>? resourceIds = null,
        IEnumerable<int>? unitIds = null,
        CancellationToken cancellationToken = default);
}