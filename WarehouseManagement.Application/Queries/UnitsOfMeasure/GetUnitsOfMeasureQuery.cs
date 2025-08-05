using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.UnitsOfMeasure;

/// <summary>
/// Запрос для получения списка единиц измерения
/// </summary>
public class GetUnitsOfMeasureQuery : IRequest<IEnumerable<UnitOfMeasureDto>>
{
    /// <summary>
    /// Включать ли архивные единицы измерения
    /// </summary>
    public bool IncludeArchived { get; set; } = false;
}