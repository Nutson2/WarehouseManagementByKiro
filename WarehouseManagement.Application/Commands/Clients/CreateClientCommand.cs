using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.Clients;

/// <summary>
/// Команда для создания клиента
/// </summary>
public class CreateClientCommand : IRequest<ClientDto>
{
    /// <summary>
    /// Наименование клиента
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Адрес клиента
    /// </summary>
    public string Address { get; set; } = string.Empty;
}