using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.ShipmentDocuments;

/// <summary>
/// Обработчик запроса получения документа отгрузки по идентификатору
/// </summary>
public class GetShipmentDocumentByIdQueryHandler : IRequestHandler<GetShipmentDocumentByIdQuery, ShipmentDocumentDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetShipmentDocumentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShipmentDocumentDto?> Handle(GetShipmentDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await _unitOfWork.ShipmentDocuments.GetWithResourcesAndClientAsync(request.Id, cancellationToken);
        
        return document != null ? _mapper.Map<ShipmentDocumentDto>(document) : null;
    }
}