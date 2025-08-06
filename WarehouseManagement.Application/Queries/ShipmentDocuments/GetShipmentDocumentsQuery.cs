using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.ShipmentDocuments;

/// <summary>
/// Запрос для получения отфильтрованных документов отгрузки
/// </summary>
public class GetShipmentDocumentsQuery : IRequest<IEnumerable<ShipmentDocumentDto>>
{
    /// <summary>
    /// Дата начала периода (включительно)
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Дата окончания периода (включительно)
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Номера документов для фильтрации
    /// </summary>
    public IEnumerable<string>? Numbers { get; set; }

    /// <summary>
    /// Идентификаторы ресурсов для фильтрации
    /// </summary>
    public IEnumerable<int>? ResourceIds { get; set; }

    /// <summary>
    /// Идентификаторы единиц измерения для фильтрации
    /// </summary>
    public IEnumerable<int>? UnitIds { get; set; }
}