using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.ReceiptDocuments;

/// <summary>
/// Запрос для получения документа поступления по идентификатору
/// </summary>
public class GetReceiptDocumentByIdQuery : IRequest<ReceiptDocumentDto?>
{
    /// <summary>
    /// Идентификатор документа поступления
    /// </summary>
    public int Id { get; set; }
}