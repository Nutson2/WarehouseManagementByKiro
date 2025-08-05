using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.UnitsOfMeasure;

/// <summary>
/// Обработчик запроса получения единицы измерения по идентификатору
/// </summary>
public class GetUnitOfMeasureByIdQueryHandler : IRequestHandler<GetUnitOfMeasureByIdQuery, UnitOfMeasureDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUnitOfMeasureByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UnitOfMeasureDto?> Handle(GetUnitOfMeasureByIdQuery request, CancellationToken cancellationToken)
    {
        var unitOfMeasure = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(request.Id, cancellationToken);
        
        return unitOfMeasure != null ? _mapper.Map<UnitOfMeasureDto>(unitOfMeasure) : null;
    }
}