using MediatR;

namespace WarehouseManagement.Application.Commands.ReceiptDocuments;

/// <summary>
/// Команда для удаления документа поступления
/// </summary>
public class DeleteReceiptDocumentCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public int Id { get; set; }
}