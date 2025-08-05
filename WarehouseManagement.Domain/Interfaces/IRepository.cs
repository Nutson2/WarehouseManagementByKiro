using System.Linq.Expressions;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория для работы с сущностями
/// </summary>
/// <typeparam name="T">Тип сущности, наследующей от BaseEntity</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность или null, если не найдена</returns>
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция всех сущностей</returns>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Найти сущности по условию
    /// </summary>
    /// <param name="predicate">Условие поиска</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция найденных сущностей</returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Найти первую сущность по условию
    /// </summary>
    /// <param name="predicate">Условие поиска</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Первая найденная сущность или null</returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить существование сущности по условию
    /// </summary>
    /// <param name="predicate">Условие проверки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True, если сущность существует</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить новую сущность
    /// </summary>
    /// <param name="entity">Сущность для добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавленная сущность</returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить коллекцию сущностей
    /// </summary>
    /// <param name="entities">Коллекция сущностей для добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить сущность
    /// </summary>
    /// <param name="entity">Сущность для обновления</param>
    void Update(T entity);

    /// <summary>
    /// Обновить коллекцию сущностей
    /// </summary>
    /// <param name="entities">Коллекция сущностей для обновления</param>
    void UpdateRange(IEnumerable<T> entities);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    /// <param name="entity">Сущность для удаления</param>
    void Remove(T entity);

    /// <summary>
    /// Удалить коллекцию сущностей
    /// </summary>
    /// <param name="entities">Коллекция сущностей для удаления</param>
    void RemoveRange(IEnumerable<T> entities);

    /// <summary>
    /// Получить количество сущностей по условию
    /// </summary>
    /// <param name="predicate">Условие подсчета</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество сущностей</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}