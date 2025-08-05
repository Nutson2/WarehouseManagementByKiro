using MediatR;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Команда для удаления документа отгрузки
/// </summary>
public class DeleteShipmentDocumentCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public int Id { get; set; }
}