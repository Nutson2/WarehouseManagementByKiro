using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.Resources;

/// <summary>
/// Команда для обновления ресурса
/// </summary>
public class UpdateResourceCommand : IRequest<ResourceDto>
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string Name { get; set; } = string.Empty;
}