using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.ShipmentDocuments;

/// <summary>
/// Обработчик команды обновления документа отгрузки
/// </summary>
public class UpdateShipmentDocumentCommandHandler : IRequestHandler<UpdateShipmentDocumentCommand, ShipmentDocumentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateShipmentDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShipmentDocumentDto> Handle(UpdateShipmentDocumentCommand request, CancellationToken cancellationToken)
    {
        // Получаем существующий документ с ресурсами и клиентом
        var existingDocument = await _unitOfWork.ShipmentDocuments.GetWithResourcesAndClientAsync(request.Id, cancellationToken);
        if (existingDocument == null)
        {
            throw new BusinessRuleViolationException($"Документ отгрузки с идентификатором {request.Id} не найден");
        }

        // Проверяем, что документ можно редактировать (только черновики)
        if (existingDocument.DocumentStatus == DocumentStatus.Approved)
        {
            throw new BusinessRuleViolationException("Нельзя редактировать подписанный документ отгрузки");
        }

        // Проверяем уникальность номера документа (исключая текущий документ)
        if (await _unitOfWork.ShipmentDocuments.ExistsByNumberAsync(request.Number, request.Id, cancellationToken))
        {
            throw new DuplicateNameException("Документ отгрузки", request.Number);
        }

        // Проверяем существование клиента
        var client = await _unitOfWork.Clients.GetByIdAsync(request.ClientId, cancellationToken);
        if (client == null || client.Status == EntityStatus.Archived)
        {
            throw new BusinessRuleViolationException($"Клиент с идентификатором {request.ClientId} не найден или архивирован");
        }

        // Валидируем новые данные
        if (string.IsNullOrWhiteSpace(request.Number))
        {
            throw new BusinessRuleViolationException("Номер документа отгрузки не может быть пустым");
        }

        if (request.Date == default || request.Date > DateTime.Now)
        {
            throw new BusinessRuleViolationException("Дата документа отгрузки некорректна");
        }

        // Валидируем, что документ отгрузки не пустой
        if (request.Resources.Count == 0)
        {
            throw new BusinessRuleViolationException("Документ отгрузки не может быть пустым");
        }

        // Обновляем основные свойства документа
        existingDocument.Number = request.Number.Trim();
        existingDocument.ClientId = request.ClientId;
        existingDocument.Date = request.Date;

        // Очищаем старые ресурсы
        existingDocument.Resources.Clear();

        // Добавляем новые ресурсы
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

        // Обновляем документ в репозитории
        _unitOfWork.ShipmentDocuments.Update(existingDocument);

        // Сохраняем изменения (баланс НЕ изменяется при редактировании документа отгрузки)
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Получаем обновленный документ с навигационными свойствами для маппинга
        var updatedDocument = await _unitOfWork.ShipmentDocuments.GetWithResourcesAndClientAsync(existingDocument.Id, cancellationToken);
        
        // Возвращаем DTO
        return _mapper.Map<ShipmentDocumentDto>(updatedDocument);
    }
}