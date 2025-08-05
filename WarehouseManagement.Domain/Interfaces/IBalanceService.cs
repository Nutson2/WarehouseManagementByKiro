using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

/// <summary>
/// Интерфейс доменного сервиса для управления балансом склада
/// </summary>
public interface IBalanceService
{
    /// <summary>
    /// Обновляет баланс ресурса на складе
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <param name="unitOfMeasureId">Идентификатор единицы измерения</param>
    /// <param name="quantityChange">Изменение количества (положительное для увеличения, отрицательное для уменьшения)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленный баланс</returns>
    Task<Balance> UpdateBalanceAsync(int resourceId, int unitOfMeasureId, decimal quantityChange, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет достаточность ресурсов на складе для операции
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <param name="unitOfMeasureId">Идентификатор единицы измерения</param>
    /// <param name="requiredQuantity">Требуемое количество</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="InsufficientBalanceException">Выбрасывается при недостаточном количестве ресурсов</exception>
    Task ValidateBalanceAvailabilityAsync(int resourceId, int unitOfMeasureId, decimal requiredQuantity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает текущий баланс ресурса на складе
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <param name="unitOfMeasureId">Идентификатор единицы измерения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Текущее количество ресурса или 0, если баланс не найден</returns>
    Task<decimal> GetCurrentBalanceAsync(int resourceId, int unitOfMeasureId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обрабатывает изменения баланса при операциях с документом поступления
    /// </summary>
    /// <param name="receiptResources">Ресурсы документа поступления</param>
    /// <param name="isReversal">Флаг отката операции (true для удаления документа)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task ProcessReceiptDocumentBalanceAsync(IEnumerable<ReceiptResource> receiptResources, bool isReversal = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обрабатывает изменения баланса при операциях с документом отгрузки
    /// </summary>
    /// <param name="shipmentResources">Ресурсы документа отгрузки</param>
    /// <param name="isApproval">Флаг подписания документа (true для подписания, false для отзыва)</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task ProcessShipmentDocumentBalanceAsync(IEnumerable<ShipmentResource> shipmentResources, bool isApproval, CancellationToken cancellationToken = default);
}