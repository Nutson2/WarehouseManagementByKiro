namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при попытке операции с архивной сущностью
/// </summary>
public class InvalidEntityStatusException : DomainException
{
    public string EntityType { get; }
    public int EntityId { get; }
    
    public InvalidEntityStatusException(string entityType, int entityId) 
        : base($"Операция невозможна. Сущность типа '{entityType}' с ID {entityId} находится в архиве.")
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}