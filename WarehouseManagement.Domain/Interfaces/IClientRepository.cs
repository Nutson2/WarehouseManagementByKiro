using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с клиентами
/// </summary>
public interface IClientRepository : IRepository<Client>
{
    /// <summary>
    /// Проверить существование клиента с указанным наименованием
    /// </summary>
    /// <param name="name">Наименование клиента</param>
    /// <param name="excludeId">Идентификатор клиента для исключения из проверки (для редактирования)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True, если клиент с таким наименованием существует</returns>
    Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить всех активных клиентов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция активных клиентов</returns>
    Task<IEnumerable<Client>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить, используется ли клиент в документах отгрузки
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True, если клиент используется</returns>
    Task<bool> IsUsedInDocumentsAsync(int clientId, CancellationToken cancellationToken = default);
}