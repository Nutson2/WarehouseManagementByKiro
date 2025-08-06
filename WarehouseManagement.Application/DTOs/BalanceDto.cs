using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Application.DTOs;

/// <summary>
/// DTO для баланса склада
/// </summary>
public class BalanceDto
{
    /// <summary>
    /// Идентификатор баланса
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
    /// Статус ресурса
    /// </summary>
    public EntityStatus ResourceStatus { get; set; }

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int UnitOfMeasureId { get; set; }

    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string UnitOfMeasureName { get; set; } = string.Empty;

    /// <summary>
    /// Статус единицы измерения
    /// </summary>
    public EntityStatus UnitOfMeasureStatus { get; set; }

    /// <summary>
    /// Количество ресурса на складе
    /// </summary>
    public decimal Quantity { get; set; }
}