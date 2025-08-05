using MediatR;

namespace WarehouseManagement.Application.Commands.UnitsOfMeasure;

/// <summary>
/// Команда для удаления единицы измерения
/// </summary>
public class DeleteUnitOfMeasureCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int Id { get; set; }
}