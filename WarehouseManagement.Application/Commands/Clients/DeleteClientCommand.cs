using MediatR;

namespace WarehouseManagement.Application.Commands.Clients;

/// <summary>
/// Команда для удаления клиента
/// </summary>
public class DeleteClientCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; set; }
}