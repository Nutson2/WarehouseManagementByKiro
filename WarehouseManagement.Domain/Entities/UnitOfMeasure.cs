namespace WarehouseManagement.Domain.Entities;

/// <summary>
/// Единица измерения
/// </summary>
public class UnitOfMeasure : BaseEntity
{
    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Проверяет, что наименование единицы измерения не пустое
    /// </summary>
    public bool IsValidName()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }
}