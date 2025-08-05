using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.UnitsOfMeasure;

/// <summary>
/// Обработчик команды архивирования единицы измерения
/// </summary>
public class ArchiveUnitOfMeasureCommandHandler : IRequestHandler<ArchiveUnitOfMeasureCommand, UnitOfMeasureDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ArchiveUnitOfMeasureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UnitOfMeasureDto> Handle(ArchiveUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        // Получаем единицу измерения
        var unitOfMeasure = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(request.Id, cancellationToken);
        if (unitOfMeasure == null)
        {
            throw new BusinessRuleViolationException($"Единица измерения с идентификатором {request.Id} не найдена");
        }

        // Проверяем, что единица измерения не уже в архиве
        if (unitOfMeasure.Status == EntityStatus.Archived)
        {
            throw new InvalidEntityStatusException("Единица измерения", request.Id);
        }

        // Переводим в архив
        unitOfMeasure.Status = EntityStatus.Archived;

        // Сохраняем изменения
        _unitOfWork.UnitsOfMeasure.Update(unitOfMeasure);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<UnitOfMeasureDto>(unitOfMeasure);
    }
}