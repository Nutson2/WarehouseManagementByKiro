using FluentValidation;
using WarehouseManagement.Application.Commands.Resources;

namespace WarehouseManagement.Application.Validators.Resources;

/// <summary>
/// Валидатор для команды создания ресурса
/// </summary>
public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
{
    public CreateResourceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Наименование ресурса не может быть пустым")
            .MaximumLength(255)
            .WithMessage("Наименование ресурса не может превышать 255 символов");
    }
}