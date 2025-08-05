using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.Resources;

/// <summary>
/// Запрос для получения списка ресурсов
/// </summary>
public class GetResourcesQuery : IRequest<IEnumerable<ResourceDto>>
{
    /// <summary>
    /// Включать ли архивные ресурсы
    /// </summary>
    public bool IncludeArchived { get; set; } = false;
}