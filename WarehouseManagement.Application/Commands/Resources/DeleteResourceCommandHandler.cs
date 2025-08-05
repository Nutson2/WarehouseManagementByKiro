using MediatR;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.Resources;

/// <summary>
/// Обработчик команды удаления ресурса
/// </summary>
public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteResourceCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        // Получаем ресурс
        var resource = await _unitOfWork.Resources.GetByIdAsync(request.Id, cancellationToken);
        if (resource == null)
        {
            throw new BusinessRuleViolationException($"Ресурс с идентификатором {request.Id} не найден");
        }

        // Проверяем, используется ли ресурс в документах
        if (await _unitOfWork.Resources.IsUsedInDocumentsAsync(request.Id, cancellationToken))
        {
            throw new EntityInUseException("Ресурс", request.Id);
        }

        // Удаляем ресурс
        _unitOfWork.Resources.Remove(resource);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}