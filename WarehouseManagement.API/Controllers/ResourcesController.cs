using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.API.Attributes;
using WarehouseManagement.Application.Commands.Resources;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Queries.Resources;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Контроллер для управления ресурсами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResourcesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список всех ресурсов
    /// </summary>
    /// <param name="includeArchived">Включать ли архивные ресурсы</param>
    /// <returns>Список ресурсов</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResources([FromQuery] bool includeArchived = false)
    {
        var query = new GetResourcesQuery { IncludeArchived = includeArchived };
        var resources = await _mediator.Send(query);
        return Ok(resources);
    }

    /// <summary>
    /// Получить ресурс по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>Ресурс</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ResourceDto>> GetResource([PositiveInteger] int id)
    {
        var query = new GetResourceByIdQuery { Id = id };
        var resource = await _mediator.Send(query);
        
        if (resource == null)
        {
            throw new EntityNotFoundException("Resource", id);
        }
        
        return Ok(resource);
    }

    /// <summary>
    /// Создать новый ресурс
    /// </summary>
    /// <param name="createResourceDto">Данные для создания ресурса</param>
    /// <returns>Созданный ресурс</returns>
    [HttpPost]
    [ValidateModel]
    public async Task<ActionResult<ResourceDto>> CreateResource([FromBody] CreateResourceDto createResourceDto)
    {
        var command = new CreateResourceCommand { Name = createResourceDto.Name };
        var resource = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, resource);
    }

    /// <summary>
    /// Обновить ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <param name="updateResourceDto">Данные для обновления ресурса</param>
    /// <returns>Обновленный ресурс</returns>
    [HttpPut("{id}")]
    [ValidateModel]
    public async Task<ActionResult<ResourceDto>> UpdateResource([PositiveInteger] int id, [FromBody] UpdateResourceDto updateResourceDto)
    {
        var command = new UpdateResourceCommand { Id = id, Name = updateResourceDto.Name };
        var resource = await _mediator.Send(command);
        
        return Ok(resource);
    }

    /// <summary>
    /// Удалить ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteResource([PositiveInteger] int id)
    {
        var command = new DeleteResourceCommand { Id = id };
        await _mediator.Send(command);
        
        return NoContent();
    }

    /// <summary>
    /// Архивировать ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>Архивированный ресурс</returns>
    [HttpPut("{id}/archive")]
    public async Task<ActionResult<ResourceDto>> ArchiveResource([PositiveInteger] int id)
    {
        var command = new ArchiveResourceCommand { Id = id };
        var resource = await _mediator.Send(command);
        
        return Ok(resource);
    }
}