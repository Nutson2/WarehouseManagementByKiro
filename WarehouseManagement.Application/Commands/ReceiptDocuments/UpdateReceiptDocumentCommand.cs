using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.ReceiptDocuments;

/// <summary>
/// Команда для обновления документа поступления
/// </summary>
public class UpdateReceiptDocumentCommand : IRequest<ReceiptDocumentDto>
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер документа
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Дата документа
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Ресурсы в документе поступления
    /// </summary>
    public List<UpdateReceiptResourceDto> Resources { get; set; } = new();
}

/// <summary>
/// DTO для обновления ресурса в документе поступления
/// </summary>
public class UpdateReceiptResourceDto
{
    /// <summary>
    /// Идентификатор записи ресурса (0 для новых записей)
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int ResourceId { get; set; }

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public int UnitOfMeasureId { get; set; }

    /// <summary>
    /// Количество ресурса
    /// </summary>
    public decimal Quantity { get; set; }
}