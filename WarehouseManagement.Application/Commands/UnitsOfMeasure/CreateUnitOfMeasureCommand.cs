using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.UnitsOfMeasure;

/// <summary>
/// Команда для создания единицы измерения
/// </summary>
public class CreateUnitOfMeasureCommand : IRequest<UnitOfMeasureDto>
{
    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string Name { get; set; } = string.Empty;
}