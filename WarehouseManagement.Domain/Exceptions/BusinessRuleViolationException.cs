namespace WarehouseManagement.Domain.Exceptions;

/// <summary>
/// Исключение при нарушении бизнес-правил
/// </summary>
public class BusinessRuleViolationException : DomainException
{
    public BusinessRuleViolationException(string message) : base(message)
    {
    }
    
    public BusinessRuleViolationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}