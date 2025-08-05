using FluentValidation;
using WarehouseManagement.Application.Commands.UnitsOfMeasure;

namespace WarehouseManagement.Application.Validators.UnitsOfMeasure;

/// <summary>
/// Валидатор для команды создания единицы измерения
/// </summary>
public class CreateUnitOfMeasureCommandValidator : AbstractValidator<CreateUnitOfMeasureCommand>
{
    public CreateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Наименование единицы измерения не может быть пустым")
            .MaximumLength(255)
            .WithMessage("Наименование единицы измерения не может превышать 255 символов");
    }
}