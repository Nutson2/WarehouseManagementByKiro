using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.Clients;

/// <summary>
/// Обработчик команды архивирования клиента
/// </summary>
public class ArchiveClientCommandHandler : IRequestHandler<ArchiveClientCommand, ClientDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ArchiveClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ClientDto> Handle(ArchiveClientCommand request, CancellationToken cancellationToken)
    {
        // Получаем клиента
        var client = await _unitOfWork.Clients.GetByIdAsync(request.Id, cancellationToken);
        if (client == null)
        {
            throw new BusinessRuleViolationException($"Клиент с идентификатором {request.Id} не найден");
        }

        // Проверяем, что клиент не уже в архиве
        if (client.Status == EntityStatus.Archived)
        {
            throw new InvalidEntityStatusException("Клиент", request.Id);
        }

        // Переводим в архив
        client.Status = EntityStatus.Archived;

        // Сохраняем изменения
        _unitOfWork.Clients.Update(client);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<ClientDto>(client);
    }
}