using MediatR;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.UnitsOfMeasure;

/// <summary>
/// Обработчик команды удаления единицы измерения
/// </summary>
public class DeleteUnitOfMeasureCommandHandler : IRequestHandler<DeleteUnitOfMeasureCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUnitOfMeasureCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        // Получаем единицу измерения
        var unitOfMeasure = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(request.Id, cancellationToken);
        if (unitOfMeasure == null)
        {
            throw new BusinessRuleViolationException($"Единица измерения с идентификатором {request.Id} не найдена");
        }

        // Проверяем, используется ли единица измерения в документах
        if (await _unitOfWork.UnitsOfMeasure.IsUsedInDocumentsAsync(request.Id, cancellationToken))
        {
            throw new EntityInUseException("Единица измерения", request.Id);
        }

        // Удаляем единицу измерения
        _unitOfWork.UnitsOfMeasure.Remove(unitOfMeasure);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}