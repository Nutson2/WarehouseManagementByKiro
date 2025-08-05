namespace WarehouseManagement.Domain.Entities;

/// <summary>
/// Клиент
/// </summary>
public class Client : BaseEntity
{
    /// <summary>
    /// Наименование клиента
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Адрес клиента
    /// </summary>
    public string Address { get; set; } = string.Empty;
    
    /// <summary>
    /// Проверяет, что наименование клиента не пустое
    /// </summary>
    public bool IsValidName()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }
}