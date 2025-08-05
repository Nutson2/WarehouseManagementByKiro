namespace WarehouseManagement.Domain.Entities;

/// <summary>
/// Баланс ресурса на складе
/// </summary>
public class Balance : BaseEntity
{
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
    /// Количество ресурса на складе
    /// </summary>
    public decimal Quantity { get; set; }
    
    /// <summary>
    /// Проверяет, что количество не отрицательное
    /// </summary>
    public bool IsValidQuantity()
    {
        return Quantity >= 0;
    }
    
    /// <summary>
    /// Увеличивает количество ресурса
    /// </summary>
    /// <param name="amount">Количество для добавления</param>
    public void IncreaseQuantity(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Количество для увеличения не может быть отрицательным", nameof(amount));
            
        Quantity += amount;
    }
    
    /// <summary>
    /// Уменьшает количество ресурса
    /// </summary>
    /// <param name="amount">Количество для уменьшения</param>
    public void DecreaseQuantity(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Количество для уменьшения не может быть отрицательным", nameof(amount));
            
        if (Quantity < amount)
            throw new InvalidOperationException("Недостаточно ресурсов на складе");
            
        Quantity -= amount;
    }
}