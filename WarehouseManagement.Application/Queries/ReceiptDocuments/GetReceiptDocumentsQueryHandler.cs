using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.ReceiptDocuments;

/// <summary>
/// Обработчик запроса получения отфильтрованных документов поступления
/// </summary>
public class GetReceiptDocumentsQueryHandler : IRequestHandler<GetReceiptDocumentsQuery, IEnumerable<ReceiptDocumentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReceiptDocumentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReceiptDocumentDto>> Handle(GetReceiptDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = await _unitOfWork.ReceiptDocuments.GetFilteredAsync(
            request.DateFrom,
            request.DateTo,
            request.Numbers,
            request.ResourceIds,
            request.UnitIds,
            cancellationToken);

        return _mapper.Map<IEnumerable<ReceiptDocumentDto>>(documents);
    }
}