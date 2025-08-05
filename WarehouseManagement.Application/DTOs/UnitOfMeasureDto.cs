using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Application.DTOs;

/// <summary>
/// DTO для единицы измерения
/// </summary>
public class UnitOfMeasureDto
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Статус единицы измерения
    /// </summary>
    public EntityStatus Status { get; set; }
}

/// <summary>
/// DTO для создания единицы измерения
/// </summary>
public class CreateUnitOfMeasureDto
{
    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// DTO для обновления единицы измерения
/// </summary>
public class UpdateUnitOfMeasureDto
{
    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string Name { get; set; } = string.Empty;
}