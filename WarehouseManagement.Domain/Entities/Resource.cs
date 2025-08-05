namespace WarehouseManagement.Domain.Entities;

/// <summary>
/// Ресурс (товар) на складе
/// </summary>
public class Resource : BaseEntity
{
    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Проверяет, что наименование ресурса не пустое
    /// </summary>
    public bool IsValidName()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }
}