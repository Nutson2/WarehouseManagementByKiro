namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при попытке получения несуществующей сущности
/// </summary>
public class EntityNotFoundException : DomainException
{
    public string EntityType { get; }
    public int EntityId { get; }
    
    public EntityNotFoundException(string entityType, int entityId) 
        : base($"Сущность типа '{entityType}' с ID {entityId} не найдена")
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}