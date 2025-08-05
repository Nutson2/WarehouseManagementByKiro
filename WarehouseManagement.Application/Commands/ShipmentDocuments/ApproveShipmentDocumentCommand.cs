using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Команда для подписания документа отгрузки
/// </summary>
public class ApproveShipmentDocumentCommand : IRequest<ShipmentDocumentDto>
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public int Id { get; set; }
}