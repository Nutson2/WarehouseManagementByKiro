using FluentValidation;
using WarehouseManagement.Application.Commands.UnitsOfMeasure;

namespace WarehouseManagement.Application.Validators.UnitsOfMeasure;

/// <summary>
/// Валидатор для команды обновления единицы измерения
/// </summary>
public class UpdateUnitOfMeasureCommandValidator : AbstractValidator<UpdateUnitOfMeasureCommand>
{
    public UpdateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор единицы измерения должен быть больше 0");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Наименование единицы измерения не может быть пустым")
            .MaximumLength(255)
            .WithMessage("Наименование единицы измерения не может превышать 255 символов");
    }
}