namespace WarehouseManagement.Application.DTOs;

/// <summary>
/// DTO для документа поступления
/// </summary>
public class ReceiptDocumentDto
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
    public List<ReceiptResourceDto> Resources { get; set; } = new();
}