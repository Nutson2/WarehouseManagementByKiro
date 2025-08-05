namespace WarehouseManagement.Application.DTOs;

/// <summary>
/// DTO для ресурса в документе поступления
/// </summary>
public class ReceiptResourceDto
{
    /// <summary>
    /// Идентификатор записи ресурса
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int ResourceId { get; set; }

    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string ResourceName { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int UnitOfMeasureId { get; set; }

    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string UnitOfMeasureName { get; set; } = string.Empty;

    /// <summary>
    /// Количество ресурса
    /// </summary>
    public decimal Quantity { get; set; }
}