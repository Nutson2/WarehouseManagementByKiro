namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Базовое исключение для доменных ошибок
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
    
    protected DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}