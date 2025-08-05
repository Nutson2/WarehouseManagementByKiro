using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.Resources;

/// <summary>
/// Обработчик команды обновления ресурса
/// </summary>
public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, ResourceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateResourceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResourceDto> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        // Получаем ресурс
        var resource = await _unitOfWork.Resources.GetByIdAsync(request.Id, cancellationToken);
        if (resource == null)
        {
            throw new BusinessRuleViolationException($"Ресурс с идентификатором {request.Id} не найден");
        }

        // Проверяем уникальность наименования (исключая текущий ресурс)
        if (await _unitOfWork.Resources.ExistsByNameAsync(request.Name, request.Id, cancellationToken))
        {
            throw new DuplicateNameException("Ресурс", request.Name);
        }

        // Обновляем данные
        resource.Name = request.Name.Trim();

        // Валидируем доменную модель
        if (!resource.IsValidName())
        {
            throw new BusinessRuleViolationException("Наименование ресурса не может быть пустым");
        }

        // Сохраняем изменения
        _unitOfWork.Resources.Update(resource);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<ResourceDto>(resource);
    }
}