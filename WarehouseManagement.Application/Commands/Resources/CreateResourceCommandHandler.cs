using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.Resources;

/// <summary>
/// Обработчик команды создания ресурса
/// </summary>
public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, ResourceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateResourceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResourceDto> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        // Проверяем уникальность наименования
        if (await _unitOfWork.Resources.ExistsByNameAsync(request.Name, cancellationToken: cancellationToken))
        {
            throw new DuplicateNameException("Ресурс", request.Name);
        }

        // Создаем новый ресурс
        var resource = new Resource
        {
            Name = request.Name.Trim(),
            Status = EntityStatus.Active
        };

        // Валидируем доменную модель
        if (!resource.IsValidName())
        {
            throw new BusinessRuleViolationException("Наименование ресурса не может быть пустым");
        }

        // Добавляем в репозиторий
        await _unitOfWork.Resources.AddAsync(resource, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<ResourceDto>(resource);
    }
}