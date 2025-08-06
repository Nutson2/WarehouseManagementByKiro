using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Commands.Clients;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Queries.Clients;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    /// <param name="includeArchived">Включать ли архивных клиентов</param>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients([FromQuery] bool includeArchived = false)
    {
        var query = new GetClientsQuery { IncludeArchived = includeArchived };
        var clients = await _mediator.Send(query);
        return Ok(clients);
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Клиент</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetClient(int id)
    {
        var query = new GetClientByIdQuery { Id = id };
        var client = await _mediator.Send(query);
        
        if (client == null)
        {
            throw new EntityNotFoundException("Client", id);
        }
        
        return Ok(client);
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    /// <param name="createClientDto">Данные для создания клиента</param>
    /// <returns>Созданный клиент</returns>
    [HttpPost]
    public async Task<ActionResult<ClientDto>> CreateClient([FromBody] CreateClientDto createClientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = new { message = "Некорректные данные", details = ModelState } });
        }

        var command = new CreateClientCommand 
        { 
            Name = createClientDto.Name,
            Address = createClientDto.Address
        };
        var client = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
    }

    /// <summary>
    /// Обновить клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="updateClientDto">Данные для обновления клиента</param>
    /// <returns>Обновленный клиент</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ClientDto>> UpdateClient(int id, [FromBody] UpdateClientDto updateClientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = new { message = "Некорректные данные", details = ModelState } });
        }

        var command = new UpdateClientCommand 
        { 
            Id = id, 
            Name = updateClientDto.Name,
            Address = updateClientDto.Address
        };
        var client = await _mediator.Send(command);
        
        return Ok(client);
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteClient(int id)
    {
        var command = new DeleteClientCommand { Id = id };
        await _mediator.Send(command);
        
        return NoContent();
    }

    /// <summary>
    /// Архивировать клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Архивированный клиент</returns>
    [HttpPut("{id}/archive")]
    public async Task<ActionResult<ClientDto>> ArchiveClient(int id)
    {
        var command = new ArchiveClientCommand { Id = id };
        var client = await _mediator.Send(command);
        
        return Ok(client);
    }
}