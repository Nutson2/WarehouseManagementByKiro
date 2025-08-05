using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с ресурсами
/// </summary>
public interface IResourceRepository : IRepository<Resource>
{
    /// <summary>
    /// Проверить существование ресурса с указанным наименованием
    /// </summary>
    /// <param name="name">Наименование ресурса</param>
    /// <param name="excludeId">Идентификатор ресурса для исключения из проверки (для редактирования)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True, если ресурс с таким наименованием существует</returns>
    Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все активные ресурсы
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция активных ресурсов</returns>
    Task<IEnumerable<Resource>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить, используется ли ресурс в документах
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True, если ресурс используется</returns>
    Task<bool> IsUsedInDocumentsAsync(int resourceId, CancellationToken cancellationToken = default);
}