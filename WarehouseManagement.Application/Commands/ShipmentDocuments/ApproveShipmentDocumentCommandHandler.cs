using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Обработчик команды подписания документа отгрузки
/// </summary>
public class ApproveShipmentDocumentCommandHandler : IRequestHandler<ApproveShipmentDocumentCommand, ShipmentDocumentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBalanceService _balanceService;
    private readonly IMapper _mapper;

    public ApproveShipmentDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IBalanceService balanceService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _balanceService = balanceService;
        _mapper = mapper;
    }

    public async Task<ShipmentDocumentDto> Handle(ApproveShipmentDocumentCommand request, CancellationToken cancellationToken)
    {
        // Получаем существующий документ с ресурсами и клиентом
        var existingDocument = await _unitOfWork.ShipmentDocuments.GetWithResourcesAndClientAsync(request.Id, cancellationToken);
        if (existingDocument == null)
        {
            throw new BusinessRuleViolationException($"Документ отгрузки с идентификатором {request.Id} не найден");
        }

        // Проверяем, что документ можно подписать
        if (!existingDocument.CanBeApproved())
        {
            if (existingDocument.DocumentStatus == DocumentStatus.Approved)
            {
                throw new BusinessRuleViolationException("Документ отгрузки уже подписан");
            }
            if (!existingDocument.HasResources())
            {
                throw new BusinessRuleViolationException("Нельзя подписать пустой документ отгрузки");
            }
            throw new BusinessRuleViolationException("Документ отгрузки не может быть подписан");
        }

        // Проверяем достаточность ресурсов на складе и обновляем баланс
        await _balanceService.ProcessShipmentDocumentBalanceAsync(existingDocument.Resources, true, cancellationToken);

        // Подписываем документ
        existingDocument.Approve();

        // Обновляем документ в репозитории
        _unitOfWork.ShipmentDocuments.Update(existingDocument);

        // Сохраняем изменения
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Получаем обновленный документ с навигационными свойствами для маппинга
        var updatedDocument = await _unitOfWork.ShipmentDocuments.GetWithResourcesAndClientAsync(existingDocument.Id, cancellationToken);
        
        // Возвращаем DTO
        return _mapper.Map<ShipmentDocumentDto>(updatedDocument);
    }
}