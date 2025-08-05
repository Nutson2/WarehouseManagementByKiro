using FluentValidation;
using WarehouseManagement.Application.Commands.Resources;

namespace WarehouseManagement.Application.Validators.Resources;

/// <summary>
/// Валидатор для команды обновления ресурса
/// </summary>
public class UpdateResourceCommandValidator : AbstractValidator<UpdateResourceCommand>
{
    public UpdateResourceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор ресурса должен быть больше 0");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Наименование ресурса не может быть пустым")
            .MaximumLength(255)
            .WithMessage("Наименование ресурса не может превышать 255 символов");
    }
}