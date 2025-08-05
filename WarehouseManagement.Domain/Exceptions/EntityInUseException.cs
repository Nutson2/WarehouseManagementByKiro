namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при попытке удаления используемой сущности
/// </summary>
public class EntityInUseException : DomainException
{
    public string EntityType { get; }
    public int EntityId { get; }
    
    public EntityInUseException(string entityType, int entityId) 
        : base($"Сущность типа '{entityType}' с ID {entityId} используется и не может быть удалена. Переведите ее в архив.")
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}