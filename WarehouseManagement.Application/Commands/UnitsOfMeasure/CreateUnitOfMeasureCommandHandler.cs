using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.UnitsOfMeasure;

/// <summary>
/// Обработчик команды создания единицы измерения
/// </summary>
public class CreateUnitOfMeasureCommandHandler : IRequestHandler<CreateUnitOfMeasureCommand, UnitOfMeasureDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUnitOfMeasureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UnitOfMeasureDto> Handle(CreateUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        // Проверяем уникальность наименования
        if (await _unitOfWork.UnitsOfMeasure.ExistsByNameAsync(request.Name, cancellationToken: cancellationToken))
        {
            throw new DuplicateNameException("Единица измерения", request.Name);
        }

        // Создаем новую единицу измерения
        var unitOfMeasure = new UnitOfMeasure
        {
            Name = request.Name.Trim(),
            Status = EntityStatus.Active
        };

        // Валидируем доменную модель
        if (!unitOfMeasure.IsValidName())
        {
            throw new BusinessRuleViolationException("Наименование единицы измерения не может быть пустым");
        }

        // Добавляем в репозиторий
        await _unitOfWork.UnitsOfMeasure.AddAsync(unitOfMeasure, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<UnitOfMeasureDto>(unitOfMeasure);
    }
}