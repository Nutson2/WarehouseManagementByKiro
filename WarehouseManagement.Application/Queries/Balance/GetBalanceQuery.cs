using MediatR;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Queries.Balance;

/// <summary>
/// Запрос для получения баланса склада с фильтрацией
/// </summary>
public class GetBalanceQuery : IRequest<IEnumerable<BalanceDto>>
{
    /// <summary>
    /// Идентификаторы ресурсов для фильтрации (null - все ресурсы)
    /// </summary>
    public IEnumerable<int>? ResourceIds { get; set; }

    /// <summary>
    /// Идентификаторы единиц измерения для фильтрации (null - все единицы)
    /// </summary>
    public IEnumerable<int>? UnitIds { get; set; }
}