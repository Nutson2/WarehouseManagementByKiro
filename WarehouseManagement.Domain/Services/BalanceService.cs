using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Domain.Services;

/// <summary>
/// Доменный сервис для управления балансом склада
/// </summary>
public class BalanceService : IBalanceService
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BalanceService(IBalanceRepository balanceRepository, IUnitOfWork unitOfWork)
    {
        _balanceRepository = balanceRepository ?? throw new ArgumentNullException(nameof(balanceRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Balance> UpdateBalanceAsync(int resourceId, int unitOfMeasureId, decimal quantityChange, CancellationToken cancellationToken = default)
    {
        if (resourceId <= 0)
            throw new ArgumentException("Идентификатор ресурса должен быть положительным", nameof(resourceId));
        
        if (unitOfMeasureId <= 0)
            throw new ArgumentException("Идентификатор единицы измерения должен быть положительным", nameof(unitOfMeasureId));

        // Получаем существующий баланс или создаем новый
        var balance = await _balanceRepository.GetByResourceAndUnitAsync(resourceId, unitOfMeasureId, cancellationToken);
        
        if (balance == null)
        {
            // Создаем новый баланс, если его не существует
            if (quantityChange < 0)
            {
                throw new InsufficientBalanceException(resourceId, unitOfMeasureId, Math.Abs(quantityChange), 0);
            }

            balance = new Balance
            {
                ResourceId = resourceId,
                UnitOfMeasureId = unitOfMeasureId,
                Quantity = quantityChange,
                Status = EntityStatus.Active
            };

            await _balanceRepository.AddAsync(balance, cancellationToken);
        }
        else
        {
            // Обновляем существующий баланс
            var newQuantity = balance.Quantity + quantityChange;
            
            if (newQuantity < 0)
            {
                throw new InsufficientBalanceException(resourceId, unitOfMeasureId, Math.Abs(quantityChange), balance.Quantity);
            }

            balance.Quantity = newQuantity;
            _balanceRepository.Update(balance);
        }

        return balance;
    }

    public async Task ValidateBalanceAvailabilityAsync(int resourceId, int unitOfMeasureId, decimal requiredQuantity, CancellationToken cancellationToken = default)
    {
        if (resourceId <= 0)
            throw new ArgumentException("Идентификатор ресурса должен быть положительным", nameof(resourceId));
        
        if (unitOfMeasureId <= 0)
            throw new ArgumentException("Идентификатор единицы измерения должен быть положительным", nameof(unitOfMeasureId));

        if (requiredQuantity < 0)
            throw new ArgumentException("Требуемое количество не может быть отрицательным", nameof(requiredQuantity));

        var currentBalance = await GetCurrentBalanceAsync(resourceId, unitOfMeasureId, cancellationToken);
        
        if (currentBalance < requiredQuantity)
        {
            throw new InsufficientBalanceException(resourceId, unitOfMeasureId, requiredQuantity, currentBalance);
        }
    }

    public async Task<decimal> GetCurrentBalanceAsync(int resourceId, int unitOfMeasureId, CancellationToken cancellationToken = default)
    {
        if (resourceId <= 0)
            throw new ArgumentException("Идентификатор ресурса должен быть положительным", nameof(resourceId));
        
        if (unitOfMeasureId <= 0)
            throw new ArgumentException("Идентификатор единицы измерения должен быть положительным", nameof(unitOfMeasureId));

        var balance = await _balanceRepository.GetByResourceAndUnitAsync(resourceId, unitOfMeasureId, cancellationToken);
        return balance?.Quantity ?? 0;
    }

    public async Task ProcessReceiptDocumentBalanceAsync(IEnumerable<ReceiptResource> receiptResources, bool isReversal = false, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(receiptResources);

        var resources = receiptResources.ToList();
        if (resources.Count == 0)
            return; // Пустые документы поступления разрешены

        foreach (var resource in resources)
        {
            if (resource.Quantity <= 0)
                throw new ArgumentException($"Количество ресурса должно быть положительным. ResourceId: {resource.ResourceId}");

            // При отмене (удалении документа) уменьшаем баланс, иначе увеличиваем
            var quantityChange = isReversal ? -resource.Quantity : resource.Quantity;
            
            await UpdateBalanceAsync(resource.ResourceId, resource.UnitOfMeasureId, quantityChange, cancellationToken);
        }
    }

    public async Task ProcessShipmentDocumentBalanceAsync(IEnumerable<ShipmentResource> shipmentResources, bool isApproval, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(shipmentResources);

        var resources = shipmentResources.ToList();
        if (resources.Count == 0)
            throw new ArgumentException("Документ отгрузки не может быть пустым", nameof(shipmentResources));

        foreach (var resource in resources)
        {
            if (resource.Quantity <= 0)
                throw new ArgumentException($"Количество ресурса должно быть положительным. ResourceId: {resource.ResourceId}");

            if (isApproval)
            {
                // При подписании проверяем достаточность ресурсов и уменьшаем баланс
                await ValidateBalanceAvailabilityAsync(resource.ResourceId, resource.UnitOfMeasureId, resource.Quantity, cancellationToken);
                await UpdateBalanceAsync(resource.ResourceId, resource.UnitOfMeasureId, -resource.Quantity, cancellationToken);
            }
            else
            {
                // При отзыве увеличиваем баланс обратно
                await UpdateBalanceAsync(resource.ResourceId, resource.UnitOfMeasureId, resource.Quantity, cancellationToken);
            }
        }
    }
}