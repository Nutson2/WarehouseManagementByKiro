using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.Resources;

/// <summary>
/// Запрос для получения ресурса по идентификатору
/// </summary>
public class GetResourceByIdQuery : IRequest<ResourceDto?>
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int Id { get; set; }
}