using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.UnitsOfMeasure;

/// <summary>
/// Обработчик запроса получения списка единиц измерения
/// </summary>
public class GetUnitsOfMeasureQueryHandler : IRequestHandler<GetUnitsOfMeasureQuery, IEnumerable<UnitOfMeasureDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUnitsOfMeasureQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UnitOfMeasureDto>> Handle(GetUnitsOfMeasureQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.UnitOfMeasure> unitsOfMeasure;

        if (request.IncludeArchived)
        {
            // Получаем все единицы измерения
            unitsOfMeasure = await _unitOfWork.UnitsOfMeasure.GetAllAsync(cancellationToken);
        }
        else
        {
            // Получаем только активные единицы измерения
            unitsOfMeasure = await _unitOfWork.UnitsOfMeasure.GetActiveAsync(cancellationToken);
        }

        // Маппим в DTO
        return _mapper.Map<IEnumerable<UnitOfMeasureDto>>(unitsOfMeasure);
    }
}