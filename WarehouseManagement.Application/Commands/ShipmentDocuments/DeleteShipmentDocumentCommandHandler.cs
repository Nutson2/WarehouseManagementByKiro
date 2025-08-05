using MediatR;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Обработчик команды удаления документа отгрузки
/// </summary>
public class DeleteShipmentDocumentCommandHandler : IRequestHandler<DeleteShipmentDocumentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBalanceService _balanceService;

    public DeleteShipmentDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IBalanceService balanceService)
    {
        _unitOfWork = unitOfWork;
        _balanceService = balanceService;
    }

    public async Task<Unit> Handle(DeleteShipmentDocumentCommand request, CancellationToken cancellationToken)
    {
        // Получаем существующий документ с ресурсами
        var existingDocument = await _unitOfWork.ShipmentDocuments.GetWithResourcesAndClientAsync(request.Id, cancellationToken);
        if (existingDocument == null)
        {
            throw new BusinessRuleViolationException($"Документ отгрузки с идентификатором {request.Id} не найден");
        }

        // Если документ подписан, нужно восстановить баланс
        if (existingDocument.DocumentStatus == DocumentStatus.Approved)
        {
            await _balanceService.ProcessShipmentDocumentBalanceAsync(existingDocument.Resources, false, cancellationToken);
        }

        // Удаляем документ
        _unitOfWork.ShipmentDocuments.Remove(existingDocument);

        // Сохраняем изменения
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}