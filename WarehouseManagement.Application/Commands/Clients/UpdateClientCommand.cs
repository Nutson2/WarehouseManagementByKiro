using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.Clients;

/// <summary>
/// Команда для обновления клиента
/// </summary>
public class UpdateClientCommand : IRequest<ClientDto>
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование клиента
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Адрес клиента
    /// </summary>
    public string Address { get; set; } = string.Empty;
}