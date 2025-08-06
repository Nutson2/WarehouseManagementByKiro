using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Commands.ShipmentDocuments;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Queries.ShipmentDocuments;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Контроллер для управления документами отгрузки
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShipmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список документов отгрузки с фильтрацией
    /// </summary>
    /// <param name="dateFrom">Дата начала периода (включительно)</param>
    /// <param name="dateTo">Дата окончания периода (включительно)</param>
    /// <param name="numbers">Номера документов для фильтрации (множественный выбор)</param>
    /// <param name="resourceIds">Идентификаторы ресурсов для фильтрации (множественный выбор)</param>
    /// <param name="unitIds">Идентификаторы единиц измерения для фильтрации (множественный выбор)</param>
    /// <returns>Список документов отгрузки</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipmentDocumentDto>>> GetShipments(
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null,
        [FromQuery] string[]? numbers = null,
        [FromQuery] int[]? resourceIds = null,
        [FromQuery] int[]? unitIds = null)
    {
        var query = new GetShipmentDocumentsQuery
        {
            DateFrom = dateFrom,
            DateTo = dateTo,
            Numbers = numbers?.Length > 0 ? numbers : null,
            ResourceIds = resourceIds?.Length > 0 ? resourceIds : null,
            UnitIds = unitIds?.Length > 0 ? unitIds : null
        };

        var shipments = await _mediator.Send(query);
        return Ok(shipments);
    }

    /// <summary>
    /// Получить документ отгрузки по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <returns>Документ отгрузки</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ShipmentDocumentDto>> GetShipment(int id)
    {
        var query = new GetShipmentDocumentByIdQuery { Id = id };
        var shipment = await _mediator.Send(query);
        
        if (shipment == null)
        {
            throw new EntityNotFoundException("ShipmentDocument", id);
        }
        
        return Ok(shipment);
    }

    /// <summary>
    /// Создать новый документ отгрузки
    /// </summary>
    /// <param name="command">Данные для создания документа</param>
    /// <returns>Созданный документ отгрузки</returns>
    [HttpPost]
    public async Task<ActionResult<ShipmentDocumentDto>> CreateShipment([FromBody] CreateShipmentDocumentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = new { message = "Некорректные данные", details = ModelState } });
        }

        var shipment = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetShipment), new { id = shipment.Id }, shipment);
    }

    /// <summary>
    /// Обновить документ отгрузки
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <param name="command">Данные для обновления документа</param>
    /// <returns>Обновленный документ отгрузки</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ShipmentDocumentDto>> UpdateShipment(int id, [FromBody] UpdateShipmentDocumentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = new { message = "Некорректные данные", details = ModelState } });
        }

        command.Id = id;
        var shipment = await _mediator.Send(command);
        
        return Ok(shipment);
    }

    /// <summary>
    /// Удалить документ отгрузки
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShipment(int id)
    {
        var command = new DeleteShipmentDocumentCommand { Id = id };
        await _mediator.Send(command);
        
        return NoContent();
    }

    /// <summary>
    /// Подписать документ отгрузки
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <returns>Подписанный документ отгрузки</returns>
    [HttpPut("{id}/approve")]
    public async Task<ActionResult<ShipmentDocumentDto>> ApproveShipment(int id)
    {
        var command = new ApproveShipmentDocumentCommand { Id = id };
        var shipment = await _mediator.Send(command);
        
        return Ok(shipment);
    }

    /// <summary>
    /// Отозвать подписанный документ отгрузки
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <returns>Отозванный документ отгрузки</returns>
    [HttpPut("{id}/revoke")]
    public async Task<ActionResult<ShipmentDocumentDto>> RevokeShipment(int id)
    {
        var command = new RevokeShipmentDocumentCommand { Id = id };
        var shipment = await _mediator.Send(command);
        
        return Ok(shipment);
    }
}