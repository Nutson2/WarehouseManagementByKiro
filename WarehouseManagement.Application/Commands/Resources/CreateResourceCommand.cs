using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.Resources;

/// <summary>
/// Команда для создания ресурса
/// </summary>
public class CreateResourceCommand : IRequest<ResourceDto>
{
    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string Name { get; set; } = string.Empty;
}