namespace WarehouseManagement.Domain.Entities;

/// <summary>
/// Ресурс в документе отгрузки
/// </summary>
public class ShipmentResource : BaseEntity
{
    /// <summary>
    /// Идентификатор документа отгрузки
    /// </summary>
    public int ShipmentDocumentId { get; set; }
    
    /// <summary>
    /// Документ отгрузки
    /// </summary>
    public ShipmentDocument ShipmentDocument { get; set; } = null!;
    
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int ResourceId { get; set; }
    
    /// <summary>
    /// Ресурс
    /// </summary>
    public Resource Resource { get; set; } = null!;
    
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int UnitOfMeasureId { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public UnitOfMeasure UnitOfMeasure { get; set; } = null!;
    
    /// <summary>
    /// Количество ресурса
    /// </summary>
    public decimal Quantity { get; set; }
    
    /// <summary>
    /// Проверяет, что количество положительное
    /// </summary>
    public bool IsValidQuantity()
    {
        return Quantity > 0;
    }
}