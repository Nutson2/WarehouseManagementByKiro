using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.Resources;

/// <summary>
/// Команда для архивирования ресурса
/// </summary>
public class ArchiveResourceCommand : IRequest<ResourceDto>
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int Id { get; set; }
}