using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Обработчик команды отзыва документа отгрузки
/// </summary>
public class RevokeShipmentDocumentCommandHandler : IRequestHandler<RevokeShipmentDocumentCommand, ShipmentDocumentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBalanceService _balanceService;
    private readonly IMapper _mapper;

    public RevokeShipmentDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IBalanceService balanceService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _balanceService = balanceService;
        _mapper = mapper;
    }

    public async Task<ShipmentDocumentDto> Handle(RevokeShipmentDocumentCommand request, CancellationToken cancellationToken)
    {
        // Получаем существующий документ с ресурсами и клиентом
        var existingDocument = await _unitOfWork.ShipmentDocuments.GetWithResourcesAndClientAsync(request.Id, cancellationToken);
        if (existingDocument == null)
        {
            throw new BusinessRuleViolationException($"Документ отгрузки с идентификатором {request.Id} не найден");
        }

        // Проверяем, что документ можно отозвать
        if (!existingDocument.CanBeRevoked())
        {
            if (existingDocument.DocumentStatus == DocumentStatus.Draft)
            {
                throw new BusinessRuleViolationException("Документ отгрузки не подписан");
            }
            throw new BusinessRuleViolationException("Документ отгрузки не может быть отозван");
        }

        // Восстанавливаем баланс склада (возвращаем ресурсы)
        await _balanceService.ProcessShipmentDocumentBalanceAsync(existingDocument.Resources, false, cancellationToken);

        // Отзываем документ
        existingDocument.Revoke();

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