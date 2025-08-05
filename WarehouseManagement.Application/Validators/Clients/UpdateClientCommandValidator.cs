using FluentValidation;
using WarehouseManagement.Application.Commands.Clients;

namespace WarehouseManagement.Application.Validators.Clients;

/// <summary>
/// Валидатор для команды обновления клиента
/// </summary>
public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор клиента должен быть больше 0");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Наименование клиента не может быть пустым")
            .MaximumLength(255)
            .WithMessage("Наименование клиента не может превышать 255 символов");

        RuleFor(x => x.Address)
            .MaximumLength(500)
            .WithMessage("Адрес клиента не может превышать 500 символов");
    }
}