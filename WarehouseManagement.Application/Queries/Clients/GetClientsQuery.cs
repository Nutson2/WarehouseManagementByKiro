using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.Clients;

/// <summary>
/// Запрос для получения списка клиентов
/// </summary>
public class GetClientsQuery : IRequest<IEnumerable<ClientDto>>
{
    /// <summary>
    /// Включать ли архивных клиентов
    /// </summary>
    public bool IncludeArchived { get; set; } = false;
}