using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.ReceiptDocuments;

/// <summary>
/// Команда для создания документа поступления
/// </summary>
public class CreateReceiptDocumentCommand : IRequest<ReceiptDocumentDto>
{
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
    public List<CreateReceiptResourceDto> Resources { get; set; } = new();
}

/// <summary>
/// DTO для создания ресурса в документе поступления
/// </summary>
public class CreateReceiptResourceDto
{
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