using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Queries.Balance;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Контроллер для работы с балансом склада
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BalanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public BalanceController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Получить баланс склада с возможностью фильтрации
    /// </summary>
    /// <param name="resourceIds">Идентификаторы ресурсов для фильтрации (множественный выбор)</param>
    /// <param name="unitIds">Идентификаторы единиц измерения для фильтрации (множественный выбор)</param>
    /// <returns>Список балансов склада</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BalanceDto>>> GetBalance(
        [FromQuery] int[]? resourceIds = null,
        [FromQuery] int[]? unitIds = null)
    {
        var query = new GetBalanceQuery
        {
            ResourceIds = resourceIds?.Length > 0 ? resourceIds : null,
            UnitIds = unitIds?.Length > 0 ? unitIds : null
        };

        var balance = await _mediator.Send(query);
        return Ok(balance);
    }
}