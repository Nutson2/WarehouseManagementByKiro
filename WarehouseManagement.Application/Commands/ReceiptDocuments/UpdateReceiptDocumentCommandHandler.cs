using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.ReceiptDocuments;

/// <summary>
/// Обработчик команды обновления документа поступления
/// </summary>
public class UpdateReceiptDocumentCommandHandler : IRequestHandler<UpdateReceiptDocumentCommand, ReceiptDocumentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBalanceService _balanceService;
    private readonly IMapper _mapper;

    public UpdateReceiptDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IBalanceService balanceService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _balanceService = balanceService;
        _mapper = mapper;
    }

    public async Task<ReceiptDocumentDto> Handle(UpdateReceiptDocumentCommand request, CancellationToken cancellationToken)
    {
        // Получаем существующий документ с ресурсами
        var existingDocument = await _unitOfWork.ReceiptDocuments.GetWithResourcesAsync(request.Id, cancellationToken);
        if (existingDocument == null)
        {
            throw new BusinessRuleViolationException($"Документ поступления с идентификатором {request.Id} не найден");
        }

        // Проверяем уникальность номера документа (исключая текущий документ)
        if (await _unitOfWork.ReceiptDocuments.ExistsByNumberAsync(request.Number, request.Id, cancellationToken))
        {
            throw new DuplicateNameException("Документ поступления", request.Number);
        }

        // Валидируем новые данные
        if (string.IsNullOrWhiteSpace(request.Number))
        {
            throw new BusinessRuleViolationException("Номер документа поступления не может быть пустым");
        }

        if (request.Date == default || request.Date > DateTime.Now)
        {
            throw new BusinessRuleViolationException("Дата документа поступления некорректна");
        }

        // Сначала отменяем влияние старого документа на баланс
        await _balanceService.ProcessReceiptDocumentBalanceAsync(existingDocument.Resources, true, cancellationToken);

        // Обновляем основные свойства документа
        existingDocument.Number = request.Number.Trim();
        existingDocument.Date = request.Date;

        // Очищаем старые ресурсы
        existingDocument.Resources.Clear();

        // Добавляем новые ресурсы (пустые документы поступления разрешены)
        foreach (var resourceDto in request.Resources)
        {
            if (resourceDto.Quantity <= 0)
            {
                throw new BusinessRuleViolationException($"Количество ресурса должно быть положительным");
            }

            // Проверяем существование ресурса и единицы измерения
            var resource = await _unitOfWork.Resources.GetByIdAsync(resourceDto.ResourceId, cancellationToken);
            if (resource == null || resource.Status == EntityStatus.Archived)
            {
                throw new BusinessRuleViolationException($"Ресурс с идентификатором {resourceDto.ResourceId} не найден или архивирован");
            }

            var unitOfMeasure = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(resourceDto.UnitOfMeasureId, cancellationToken);
            if (unitOfMeasure == null || unitOfMeasure.Status == EntityStatus.Archived)
            {
                throw new BusinessRuleViolationException($"Единица измерения с идентификатором {resourceDto.UnitOfMeasureId} не найдена или архивирована");
            }

            existingDocument.AddResource(resourceDto.ResourceId, resourceDto.UnitOfMeasureId, resourceDto.Quantity);
        }

        // Применяем влияние нового документа на баланс
        await _balanceService.ProcessReceiptDocumentBalanceAsync(existingDocument.Resources, false, cancellationToken);

        // Обновляем документ в репозитории
        _unitOfWork.ReceiptDocuments.Update(existingDocument);

        // Сохраняем изменения
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Получаем обновленный документ с навигационными свойствами для маппинга
        var updatedDocument = await _unitOfWork.ReceiptDocuments.GetWithResourcesAsync(existingDocument.Id, cancellationToken);
        
        // Возвращаем DTO
        return _mapper.Map<ReceiptDocumentDto>(updatedDocument);
    }
}