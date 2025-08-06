namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при попытке создания дублирующейся сущности
/// </summary>
public class DuplicateEntityException : DomainException
{
    public string EntityType { get; }
    public string PropertyName { get; }
    public string PropertyValue { get; }
    
    public DuplicateEntityException(string entityType, string propertyName, string propertyValue) 
        : base($"Сущность типа '{entityType}' с {propertyName} '{propertyValue}' уже существует")
    {
        EntityType = entityType;
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }
}