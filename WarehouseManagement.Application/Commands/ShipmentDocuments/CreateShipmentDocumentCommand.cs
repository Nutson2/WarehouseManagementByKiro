using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Команда для создания документа отгрузки
/// </summary>
public class CreateShipmentDocumentCommand : IRequest<ShipmentDocumentDto>
{
    /// <summary>
    /// Номер документа
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Дата документа
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Ресурсы в документе отгрузки
    /// </summary>
    public List<CreateShipmentResourceDto> Resources { get; set; } = new();
}

/// <summary>
/// DTO для создания ресурса в документе отгрузки
/// </summary>
public class CreateShipmentResourceDto
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