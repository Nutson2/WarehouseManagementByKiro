using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Commands.UnitsOfMeasure;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Queries.UnitsOfMeasure;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Контроллер для управления единицами измерения
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UnitsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список всех единиц измерения
    /// </summary>
    /// <param name="includeArchived">Включать ли архивные единицы измерения</param>
    /// <returns>Список единиц измерения</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitOfMeasureDto>>> GetUnits([FromQuery] bool includeArchived = false)
    {
        var query = new GetUnitsOfMeasureQuery { IncludeArchived = includeArchived };
        var units = await _mediator.Send(query);
        return Ok(units);
    }

    /// <summary>
    /// Получить единицу измерения по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <returns>Единица измерения</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitOfMeasureDto>> GetUnit(int id)
    {
        var query = new GetUnitOfMeasureByIdQuery { Id = id };
        var unit = await _mediator.Send(query);
        
        if (unit == null)
        {
            throw new EntityNotFoundException("UnitOfMeasure", id);
        }
        
        return Ok(unit);
    }

    /// <summary>
    /// Создать новую единицу измерения
    /// </summary>
    /// <param name="createUnitDto">Данные для создания единицы измерения</param>
    /// <returns>Созданная единица измерения</returns>
    [HttpPost]
    public async Task<ActionResult<UnitOfMeasureDto>> CreateUnit([FromBody] CreateUnitOfMeasureDto createUnitDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = new { message = "Некорректные данные", details = ModelState } });
        }

        var command = new CreateUnitOfMeasureCommand { Name = createUnitDto.Name };
        var unit = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetUnit), new { id = unit.Id }, unit);
    }

    /// <summary>
    /// Обновить единицу измерения
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <param name="updateUnitDto">Данные для обновления единицы измерения</param>
    /// <returns>Обновленная единица измерения</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitOfMeasureDto>> UpdateUnit(int id, [FromBody] UpdateUnitOfMeasureDto updateUnitDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = new { message = "Некорректные данные", details = ModelState } });
        }

        var command = new UpdateUnitOfMeasureCommand { Id = id, Name = updateUnitDto.Name };
        var unit = await _mediator.Send(command);
        
        return Ok(unit);
    }

    /// <summary>
    /// Удалить единицу измерения
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUnit(int id)
    {
        var command = new DeleteUnitOfMeasureCommand { Id = id };
        await _mediator.Send(command);
        
        return NoContent();
    }

    /// <summary>
    /// Архивировать единицу измерения
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <returns>Архивированная единица измерения</returns>
    [HttpPut("{id}/archive")]
    public async Task<ActionResult<UnitOfMeasureDto>> ArchiveUnit(int id)
    {
        var command = new ArchiveUnitOfMeasureCommand { Id = id };
        var unit = await _mediator.Send(command);
        
        return Ok(unit);
    }
}