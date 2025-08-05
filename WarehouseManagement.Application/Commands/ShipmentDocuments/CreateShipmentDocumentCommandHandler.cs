using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Обработчик команды создания документа отгрузки
/// </summary>
public class CreateShipmentDocumentCommandHandler : IRequestHandler<CreateShipmentDocumentCommand, ShipmentDocumentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateShipmentDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShipmentDocumentDto> Handle(CreateShipmentDocumentCommand request, CancellationToken cancellationToken)
    {
        // Проверяем уникальность номера документа
        if (await _unitOfWork.ShipmentDocuments.ExistsByNumberAsync(request.Number, cancellationToken: cancellationToken))
        {
            throw new DuplicateNameException("Документ отгрузки", request.Number);
        }

        // Проверяем существование клиента
        var client = await _unitOfWork.Clients.GetByIdAsync(request.ClientId, cancellationToken);
        if (client == null || client.Status == EntityStatus.Archived)
        {
            throw new BusinessRuleViolationException($"Клиент с идентификатором {request.ClientId} не найден или архивирован");
        }

        // Создаем новый документ отгрузки
        var shipmentDocument = new ShipmentDocument
        {
            Number = request.Number.Trim(),
            ClientId = request.ClientId,
            Date = request.Date,
            DocumentStatus = DocumentStatus.Draft,
            Status = EntityStatus.Active
        };

        // Валидируем доменную модель
        if (!shipmentDocument.IsValidNumber())
        {
            throw new BusinessRuleViolationException("Номер документа отгрузки не может быть пустым");
        }

        if (!shipmentDocument.IsValidDate())
        {
            throw new BusinessRuleViolationException("Дата документа отгрузки некорректна");
        }

        // Валидируем, что документ отгрузки не пустой
        if (request.Resources.Count == 0)
        {
            throw new BusinessRuleViolationException("Документ отгрузки не может быть пустым");
        }

        // Добавляем ресурсы в документ
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

            shipmentDocument.AddResource(resourceDto.ResourceId, resourceDto.UnitOfMeasureId, resourceDto.Quantity);
        }

        // Добавляем документ в репозиторий
        await _unitOfWork.ShipmentDocuments.AddAsync(shipmentDocument, cancellationToken);

        // Сохраняем изменения (баланс НЕ изменяется при создании документа отгрузки)
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Получаем созданный документ с навигационными свойствами для маппинга
        var createdDocument = await _unitOfWork.ShipmentDocuments.GetWithResourcesAndClientAsync(shipmentDocument.Id, cancellationToken);
        
        // Возвращаем DTO
        return _mapper.Map<ShipmentDocumentDto>(createdDocument);
    }
}