namespace WarehouseManagement.Application.DTOs;

/// <summary>
/// DTO для ресурса в документе отгрузки
/// </summary>
public class ShipmentResourceDto
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
    /// Ресурс
    /// </summary>
    public ResourceDto? Resource { get; set; }

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int UnitOfMeasureId { get; set; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public UnitOfMeasureDto? UnitOfMeasure { get; set; }

    /// <summary>
    /// Количество ресурса
    /// </summary>
    public decimal Quantity { get; set; }
}