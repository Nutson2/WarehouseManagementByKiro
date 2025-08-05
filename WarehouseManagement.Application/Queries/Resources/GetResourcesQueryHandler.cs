using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.Resources;

/// <summary>
/// Обработчик запроса получения списка ресурсов
/// </summary>
public class GetResourcesQueryHandler : IRequestHandler<GetResourcesQuery, IEnumerable<ResourceDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetResourcesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ResourceDto>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Resource> resources;

        if (request.IncludeArchived)
        {
            // Получаем все ресурсы
            resources = await _unitOfWork.Resources.GetAllAsync(cancellationToken);
        }
        else
        {
            // Получаем только активные ресурсы
            resources = await _unitOfWork.Resources.GetActiveAsync(cancellationToken);
        }

        // Маппим в DTO
        return _mapper.Map<IEnumerable<ResourceDto>>(resources);
    }
}