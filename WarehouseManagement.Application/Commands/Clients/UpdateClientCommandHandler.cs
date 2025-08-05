using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.Clients;

/// <summary>
/// Обработчик команды обновления клиента
/// </summary>
public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ClientDto> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        // Получаем клиента
        var client = await _unitOfWork.Clients.GetByIdAsync(request.Id, cancellationToken);
        if (client == null)
        {
            throw new BusinessRuleViolationException($"Клиент с идентификатором {request.Id} не найден");
        }

        // Проверяем уникальность наименования (исключая текущего клиента)
        if (await _unitOfWork.Clients.ExistsByNameAsync(request.Name, request.Id, cancellationToken))
        {
            throw new DuplicateNameException("Клиент", request.Name);
        }

        // Обновляем данные
        client.Name = request.Name.Trim();
        client.Address = request.Address.Trim();

        // Валидируем доменную модель
        if (!client.IsValidName())
        {
            throw new BusinessRuleViolationException("Наименование клиента не может быть пустым");
        }

        // Сохраняем изменения
        _unitOfWork.Clients.Update(client);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<ClientDto>(client);
    }
}