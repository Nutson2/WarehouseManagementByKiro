using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.Balance;

/// <summary>
/// Обработчик запроса для получения баланса склада
/// </summary>
public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, IEnumerable<BalanceDto>>
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly IMapper _mapper;

    public GetBalanceQueryHandler(IBalanceRepository balanceRepository, IMapper mapper)
    {
        _balanceRepository = balanceRepository ?? throw new ArgumentNullException(nameof(balanceRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<BalanceDto>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        var balances = await _balanceRepository.GetFilteredAsync(
            request.ResourceIds, 
            request.UnitIds, 
            cancellationToken);

        return _mapper.Map<IEnumerable<BalanceDto>>(balances);
    }
}