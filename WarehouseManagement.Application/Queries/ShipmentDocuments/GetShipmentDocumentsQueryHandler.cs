using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.ShipmentDocuments;

/// <summary>
/// Обработчик запроса получения отфильтрованных документов отгрузки
/// </summary>
public class GetShipmentDocumentsQueryHandler : IRequestHandler<GetShipmentDocumentsQuery, IEnumerable<ShipmentDocumentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetShipmentDocumentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShipmentDocumentDto>> Handle(GetShipmentDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = await _unitOfWork.ShipmentDocuments.GetFilteredAsync(
            request.DateFrom,
            request.DateTo,
            request.Numbers,
            request.ResourceIds,
            request.UnitIds,
            cancellationToken);

        return _mapper.Map<IEnumerable<ShipmentDocumentDto>>(documents);
    }
}