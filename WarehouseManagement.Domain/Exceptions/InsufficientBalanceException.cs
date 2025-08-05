namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при недостаточном балансе ресурса на складе
/// </summary>
public class InsufficientBalanceException : DomainException
{
    public int ResourceId { get; }
    public int UnitOfMeasureId { get; }
    public decimal RequiredQuantity { get; }
    public decimal AvailableQuantity { get; }
    
    public InsufficientBalanceException(int resourceId, int unitOfMeasureId, decimal requiredQuantity, decimal availableQuantity) 
        : base($"Недостаточно ресурсов на складе. Требуется: {requiredQuantity}, доступно: {availableQuantity}")
    {
        ResourceId = resourceId;
        UnitOfMeasureId = unitOfMeasureId;
        RequiredQuantity = requiredQuantity;
        AvailableQuantity = availableQuantity;
    }
}