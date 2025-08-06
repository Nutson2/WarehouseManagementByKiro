using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Commands.ReceiptDocuments;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Queries.ReceiptDocuments;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Контроллер для управления документами поступления
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReceiptsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReceiptsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список документов поступления с фильтрацией
    /// </summary>
    /// <param name="dateFrom">Дата начала периода (включительно)</param>
    /// <param name="dateTo">Дата окончания периода (включительно)</param>
    /// <param name="numbers">Номера документов для фильтрации (множественный выбор)</param>
    /// <param name="resourceIds">Идентификаторы ресурсов для фильтрации (множественный выбор)</param>
    /// <param name="unitIds">Идентификаторы единиц измерения для фильтрации (множественный выбор)</param>
    /// <returns>Список документов поступления</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReceiptDocumentDto>>> GetReceipts(
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null,
        [FromQuery] string[]? numbers = null,
        [FromQuery] int[]? resourceIds = null,
        [FromQuery] int[]? unitIds = null)
    {
        var query = new GetReceiptDocumentsQuery
        {
            DateFrom = dateFrom,
            DateTo = dateTo,
            Numbers = numbers?.Length > 0 ? numbers : null,
            ResourceIds = resourceIds?.Length > 0 ? resourceIds : null,
            UnitIds = unitIds?.Length > 0 ? unitIds : null
        };

        var receipts = await _mediator.Send(query);
        return Ok(receipts);
    }

    /// <summary>
    /// Получить документ поступления по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <returns>Документ поступления</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ReceiptDocumentDto>> GetReceipt(int id)
    {
        var query = new GetReceiptDocumentByIdQuery { Id = id };
        var receipt = await _mediator.Send(query);
        
        if (receipt == null)
        {
            throw new EntityNotFoundException("ReceiptDocument", id);
        }
        
        return Ok(receipt);
    }

    /// <summary>
    /// Создать новый документ поступления
    /// </summary>
    /// <param name="command">Данные для создания документа</param>
    /// <returns>Созданный документ поступления</returns>
    [HttpPost]
    public async Task<ActionResult<ReceiptDocumentDto>> CreateReceipt([FromBody] CreateReceiptDocumentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = new { message = "Некорректные данные", details = ModelState } });
        }

        var receipt = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetReceipt), new { id = receipt.Id }, receipt);
    }

    /// <summary>
    /// Обновить документ поступления
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <param name="command">Данные для обновления документа</param>
    /// <returns>Обновленный документ поступления</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ReceiptDocumentDto>> UpdateReceipt(int id, [FromBody] UpdateReceiptDocumentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = new { message = "Некорректные данные", details = ModelState } });
        }

        command.Id = id;
        var receipt = await _mediator.Send(command);
        
        return Ok(receipt);
    }

    /// <summary>
    /// Удалить документ поступления
    /// </summary>
    /// <param name="id">Идентификатор документа</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReceipt(int id)
    {
        var command = new DeleteReceiptDocumentCommand { Id = id };
        await _mediator.Send(command);
        
        return NoContent();
    }
}