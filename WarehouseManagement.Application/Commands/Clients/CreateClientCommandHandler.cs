using AutoMapper;
using MediatR;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.Application.Commands.Clients;

/// <summary>
/// Обработчик команды создания клиента
/// </summary>
public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        // Проверяем уникальность наименования
        if (await _unitOfWork.Clients.ExistsByNameAsync(request.Name, cancellationToken: cancellationToken))
        {
            throw new DuplicateNameException("Клиент", request.Name);
        }

        // Создаем нового клиента
        var client = new Client
        {
            Name = request.Name.Trim(),
            Address = request.Address.Trim(),
            Status = EntityStatus.Active
        };

        // Валидируем доменную модель
        if (!client.IsValidName())
        {
            throw new BusinessRuleViolationException("Наименование клиента не может быть пустым");
        }

        // Добавляем в репозиторий
        await _unitOfWork.Clients.AddAsync(client, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращаем DTO
        return _mapper.Map<ClientDto>(client);
    }
}