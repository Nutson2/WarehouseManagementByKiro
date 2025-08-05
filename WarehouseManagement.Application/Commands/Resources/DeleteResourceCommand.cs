using MediatR;

namespace WarehouseManagement.Application.Commands.Resources;

/// <summary>
/// Команда для удаления ресурса
/// </summary>
public class DeleteResourceCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int Id { get; set; }
}