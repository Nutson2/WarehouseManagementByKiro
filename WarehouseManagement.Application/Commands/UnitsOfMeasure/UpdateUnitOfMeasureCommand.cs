using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.UnitsOfMeasure;

/// <summary>
/// Команда для обновления единицы измерения
/// </summary>
public class UpdateUnitOfMeasureCommand : IRequest<UnitOfMeasureDto>
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string Name { get; set; } = string.Empty;
}