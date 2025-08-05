using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Команда для отзыва документа отгрузки
/// </summary>
public class RevokeShipmentDocumentCommand : IRequest<ShipmentDocumentDto>
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public int Id { get; set; }
}