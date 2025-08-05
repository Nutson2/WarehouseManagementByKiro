namespace WarehouseManagement.Domain.Entities;

/// <summary>
/// Документ поступления товаров на склад
/// </summary>
public class ReceiptDocument : BaseEntity
{
    /// <summary>
    /// Номер документа
    /// </summary>
    public string Number { get; set; } = string.Empty;
    
    /// <summary>
    /// Дата документа
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Ресурсы в документе поступления
    /// </summary>
    public List<ReceiptResource> Resources { get; set; } = new();
    
    /// <summary>
    /// Проверяет, что номер документа не пустой
    /// </summary>
    public bool IsValidNumber()
    {
        return !string.IsNullOrWhiteSpace(Number);
    }
    
    /// <summary>
    /// Проверяет, что дата документа корректная
    /// </summary>
    public bool IsValidDate()
    {
        return Date != default && Date <= DateTime.Now;
    }
    
    /// <summary>
    /// Добавляет ресурс в документ
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <param name="unitOfMeasureId">Идентификатор единицы измерения</param>
    /// <param name="quantity">Количество</param>
    public void AddResource(int resourceId, int unitOfMeasureId, decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть положительным", nameof(quantity));
            
        var resource = new ReceiptResource
        {
            ResourceId = resourceId,
            UnitOfMeasureId = unitOfMeasureId,
            Quantity = quantity
        };
        
        Resources.Add(resource);
    }
    
    /// <summary>
    /// Удаляет ресурс из документа
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса для удаления</param>
    public void RemoveResource(int resourceId)
    {
        Resources.RemoveAll(r => r.Id == resourceId);
    }
}