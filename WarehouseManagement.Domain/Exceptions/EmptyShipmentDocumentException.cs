namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при попытке создания пустого документа отгрузки
/// </summary>
public class EmptyShipmentDocumentException : DomainException
{
    public EmptyShipmentDocumentException() 
        : base("Документ отгрузки не может быть пустым. Добавьте ресурсы в документ.")
    {
    }
}