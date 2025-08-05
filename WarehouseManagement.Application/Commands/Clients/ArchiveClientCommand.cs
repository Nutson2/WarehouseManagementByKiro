using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.Clients;

/// <summary>
/// Команда для архивирования клиента
/// </summary>
public class ArchiveClientCommand : IRequest<ClientDto>
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; set; }
}