using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Application.DTOs;

/// <summary>
/// DTO для документа отгрузки
/// </summary>
public class ShipmentDocumentDto
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
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public ClientDto? Client { get; set; }

    /// <summary>
    /// Дата документа
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Статус документа
    /// </summary>
    public DocumentStatus DocumentStatus { get; set; }

    /// <summary>
    /// Ресурсы в документе отгрузки
    /// </summary>
    public List<ShipmentResourceDto> Resources { get; set; } = new();
}