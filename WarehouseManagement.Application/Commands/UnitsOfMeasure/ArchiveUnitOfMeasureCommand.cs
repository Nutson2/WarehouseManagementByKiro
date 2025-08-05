using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.UnitsOfMeasure;

/// <summary>
/// Команда для архивирования единицы измерения
/// </summary>
public class ArchiveUnitOfMeasureCommand : IRequest<UnitOfMeasureDto>
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int Id { get; set; }
}