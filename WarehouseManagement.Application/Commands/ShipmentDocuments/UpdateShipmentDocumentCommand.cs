using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Команда для обновления документа отгрузки
/// </summary>
public class UpdateShipmentDocumentCommand : IRequest<ShipmentDocumentDto>
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
    /// Дата документа
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Ресурсы в документе отгрузки
    /// </summary>
    public List<UpdateShipmentResourceDto> Resources { get; set; } = new();
}

/// <summary>
/// DTO для обновления ресурса в документе отгрузки
/// </summary>
public class UpdateShipmentResourceDto
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