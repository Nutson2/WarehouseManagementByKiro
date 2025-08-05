using MediatR;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.ReceiptDocuments;

/// <summary>
/// Обработчик команды удаления документа поступления
/// </summary>
public class DeleteReceiptDocumentCommandHandler : IRequestHandler<DeleteReceiptDocumentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBalanceService _balanceService;

    public DeleteReceiptDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IBalanceService balanceService)
    {
        _unitOfWork = unitOfWork;
        _balanceService = balanceService;
    }

    public async Task<Unit> Handle(DeleteReceiptDocumentCommand request, CancellationToken cancellationToken)
    {
        // Получаем документ с ресурсами
        var document = await _unitOfWork.ReceiptDocuments.GetWithResourcesAsync(request.Id, cancellationToken);
        if (document == null)
        {
            throw new BusinessRuleViolationException($"Документ поступления с идентификатором {request.Id} не найден");
        }

        // Отменяем влияние документа на баланс (проверяем достаточность ресурсов)
        await _balanceService.ProcessReceiptDocumentBalanceAsync(document.Resources, true, cancellationToken);

        // Удаляем документ
        _unitOfWork.ReceiptDocuments.Remove(document);

        // Сохраняем изменения
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}