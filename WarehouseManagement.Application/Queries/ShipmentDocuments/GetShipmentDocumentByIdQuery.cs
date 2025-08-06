using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.ShipmentDocuments;

/// <summary>
/// Запрос для получения документа отгрузки по идентификатору
/// </summary>
public class GetShipmentDocumentByIdQuery : IRequest<ShipmentDocumentDto?>
{
    /// <summary>
    /// Идентификатор документа отгрузки
    /// </summary>
    public int Id { get; set; }
}