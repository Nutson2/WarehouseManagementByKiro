using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.UnitsOfMeasure;

/// <summary>
/// Запрос для получения единицы измерения по идентификатору
/// </summary>
public class GetUnitOfMeasureByIdQuery : IRequest<UnitOfMeasureDto?>
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int Id { get; set; }
}