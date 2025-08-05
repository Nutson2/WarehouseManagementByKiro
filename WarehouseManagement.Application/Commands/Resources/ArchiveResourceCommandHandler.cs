using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.Resources;

/// <summary>
/// Обработчик команды архивирования ресурса
/// </summary>
public class ArchiveResourceCommandHandler : IRequestHandler<ArchiveResourceCommand, ResourceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ArchiveResourceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResourceDto> Handle(ArchiveResourceCommand request, CancellationToken cancellationToken)
    {
        // Получаем ресурс
        var resource = await _unitOfWork.Resources.GetByIdAsync(request.Id, cancellationToken);
        if (resource == null)
        {
            throw new BusinessRuleViolationException($"Ресурс с идентификатором {request.Id} не найден");
        }

        // Проверяем, что ресурс не уже в архиве
        if (resource.Status == EntityStatus.Archived)
        {
            throw new InvalidEntityStatusException("Ресурс", request.Id);
        }

        // Переводим в архив
        resource.Status = EntityStatus.Archived;

        // Сохраняем изменения
        _unitOfWork.Resources.Update(resource);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<ResourceDto>(resource);
    }
}