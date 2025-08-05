using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.Clients;

/// <summary>
/// Запрос для получения клиента по идентификатору
/// </summary>
public class GetClientByIdQuery : IRequest<ClientDto?>
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; set; }
}