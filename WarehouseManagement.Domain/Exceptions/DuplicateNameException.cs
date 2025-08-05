namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при попытке создания сущности с дублирующимся наименованием
/// </summary>
public class DuplicateNameException : DomainException
{
    public string EntityType { get; }
    public string Name { get; }
    
    public DuplicateNameException(string entityType, string name) 
        : base($"Сущность типа '{entityType}' с наименованием '{name}' уже существует")
    {
        EntityType = entityType;
        Name = name;
    }
}