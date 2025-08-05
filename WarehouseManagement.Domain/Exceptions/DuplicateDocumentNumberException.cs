namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при попытке создания документа с дублирующимся номером
/// </summary>
public class DuplicateDocumentNumberException : DomainException
{
    public string DocumentType { get; }
    public string Number { get; }
    
    public DuplicateDocumentNumberException(string documentType, string number) 
        : base($"Документ типа '{documentType}' с номером '{number}' уже существует")
    {
        DocumentType = documentType;
        Number = number;
    }
}