using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.Clients;

/// <summary>
/// Обработчик запроса получения списка клиентов
/// </summary>
public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IEnumerable<ClientDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetClientsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Client> clients;

        if (request.IncludeArchived)
        {
            // Получаем всех клиентов
            clients = await _unitOfWork.Clients.GetAllAsync(cancellationToken);
        }
        else
        {
            // Получаем только активных клиентов
            clients = await _unitOfWork.Clients.GetActiveAsync(cancellationToken);
        }

        // Маппим в DTO
        return _mapper.Map<IEnumerable<ClientDto>>(clients);
    }
}