using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Domain.Entities;

/// <summary>
/// Документ отгрузки товаров со склада
/// </summary>
public class ShipmentDocument : BaseEntity
{
    /// <summary>
    /// Номер документа
    /// </summary>
    public string Number { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Клиент
    /// </summary>
    public Client Client { get; set; } = null!;
    
    /// <summary>
    /// Дата документа
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Статус документа
    /// </summary>
    public DocumentStatus DocumentStatus { get; set; } = DocumentStatus.Draft;
    
    /// <summary>
    /// Ресурсы в документе отгрузки
    /// </summary>
    public List<ShipmentResource> Resources { get; set; } = new();
    
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
    /// Проверяет, что документ не пустой (содержит ресурсы)
    /// </summary>
    public bool HasResources()
    {
        return Resources.Any();
    }
    
    /// <summary>
    /// Проверяет, можно ли подписать документ
    /// </summary>
    public bool CanBeApproved()
    {
        return DocumentStatus == DocumentStatus.Draft && HasResources();
    }
    
    /// <summary>
    /// Проверяет, можно ли отозвать документ
    /// </summary>
    public bool CanBeRevoked()
    {
        return DocumentStatus == DocumentStatus.Approved;
    }
    
    /// <summary>
    /// Подписывает документ
    /// </summary>
    public void Approve()
    {
        if (!CanBeApproved())
            throw new InvalidOperationException("Документ не может быть подписан");
            
        DocumentStatus = DocumentStatus.Approved;
    }
    
    /// <summary>
    /// Отзывает подписанный документ
    /// </summary>
    public void Revoke()
    {
        if (!CanBeRevoked())
            throw new InvalidOperationException("Документ не может быть отозван");
            
        DocumentStatus = DocumentStatus.Draft;
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
            
        var resource = new ShipmentResource
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