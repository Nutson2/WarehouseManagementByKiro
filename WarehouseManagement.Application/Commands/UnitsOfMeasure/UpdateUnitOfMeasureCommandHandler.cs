using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.UnitsOfMeasure;

/// <summary>
/// Обработчик команды обновления единицы измерения
/// </summary>
public class UpdateUnitOfMeasureCommandHandler : IRequestHandler<UpdateUnitOfMeasureCommand, UnitOfMeasureDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUnitOfMeasureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UnitOfMeasureDto> Handle(UpdateUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        // Получаем единицу измерения
        var unitOfMeasure = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(request.Id, cancellationToken);
        if (unitOfMeasure == null)
        {
            throw new BusinessRuleViolationException($"Единица измерения с идентификатором {request.Id} не найдена");
        }

        // Проверяем уникальность наименования (исключая текущую единицу измерения)
        if (await _unitOfWork.UnitsOfMeasure.ExistsByNameAsync(request.Name, request.Id, cancellationToken))
        {
            throw new DuplicateNameException("Единица измерения", request.Name);
        }

        // Обновляем данные
        unitOfMeasure.Name = request.Name.Trim();

        // Валидируем доменную модель
        if (!unitOfMeasure.IsValidName())
        {
            throw new BusinessRuleViolationException("Наименование единицы измерения не может быть пустым");
        }

        // Сохраняем изменения
        _unitOfWork.UnitsOfMeasure.Update(unitOfMeasure);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<UnitOfMeasureDto>(unitOfMeasure);
    }
}