using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.ReceiptDocuments;

/// <summary>
/// Обработчик команды создания документа поступления
/// </summary>
public class CreateReceiptDocumentCommandHandler : IRequestHandler<CreateReceiptDocumentCommand, ReceiptDocumentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBalanceService _balanceService;
    private readonly IMapper _mapper;

    public CreateReceiptDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IBalanceService balanceService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _balanceService = balanceService;
        _mapper = mapper;
    }

    public async Task<ReceiptDocumentDto> Handle(CreateReceiptDocumentCommand request, CancellationToken cancellationToken)
    {
        // Проверяем уникальность номера документа
        if (await _unitOfWork.ReceiptDocuments.ExistsByNumberAsync(request.Number, cancellationToken: cancellationToken))
        {
            throw new DuplicateNameException("Документ поступления", request.Number);
        }

        // Создаем новый документ поступления
        var receiptDocument = new ReceiptDocument
        {
            Number = request.Number.Trim(),
            Date = request.Date,
            Status = EntityStatus.Active
        };

        // Валидируем доменную модель
        if (!receiptDocument.IsValidNumber())
        {
            throw new BusinessRuleViolationException("Номер документа поступления не может быть пустым");
        }

        if (!receiptDocument.IsValidDate())
        {
            throw new BusinessRuleViolationException("Дата документа поступления некорректна");
        }

        // Добавляем ресурсы в документ (пустые документы поступления разрешены)
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

            receiptDocument.AddResource(resourceDto.ResourceId, resourceDto.UnitOfMeasureId, resourceDto.Quantity);
        }

        // Добавляем документ в репозиторий
        await _unitOfWork.ReceiptDocuments.AddAsync(receiptDocument, cancellationToken);

        // Обновляем баланс склада
        await _balanceService.ProcessReceiptDocumentBalanceAsync(receiptDocument.Resources, false, cancellationToken);

        // Сохраняем изменения
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Получаем созданный документ с навигационными свойствами для маппинга
        var createdDocument = await _unitOfWork.ReceiptDocuments.GetWithResourcesAsync(receiptDocument.Id, cancellationToken);
        
        // Возвращаем DTO
        return _mapper.Map<ReceiptDocumentDto>(createdDocument);
    }
}