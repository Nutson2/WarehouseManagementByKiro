using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Queries.ReceiptDocuments;

/// <summary>
/// Обработчик запроса получения документа поступления по идентификатору
/// </summary>
public class GetReceiptDocumentByIdQueryHandler : IRequestHandler<GetReceiptDocumentByIdQuery, ReceiptDocumentDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReceiptDocumentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReceiptDocumentDto?> Handle(GetReceiptDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await _unitOfWork.ReceiptDocuments.GetWithResourcesAsync(request.Id, cancellationToken);
        
        return document == null ? null : _mapper.Map<ReceiptDocumentDto>(document);
    }
}