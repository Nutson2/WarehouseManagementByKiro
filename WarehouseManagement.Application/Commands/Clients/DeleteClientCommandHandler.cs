using MediatR;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.Clients;

/// <summary>
/// Обработчик команды удаления клиента
/// </summary>
public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        // Получаем клиента
        var client = await _unitOfWork.Clients.GetByIdAsync(request.Id, cancellationToken);
        if (client == null)
        {
            throw new BusinessRuleViolationException($"Клиент с идентификатором {request.Id} не найден");
        }

        // Проверяем, используется ли клиент в документах отгрузки
        if (await _unitOfWork.Clients.IsUsedInDocumentsAsync(request.Id, cancellationToken))
        {
            throw new EntityInUseException("Клиент", request.Id);
        }

        // Удаляем клиента
        _unitOfWork.Clients.Remove(client);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}