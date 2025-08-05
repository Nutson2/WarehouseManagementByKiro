using FluentValidation;
using WarehouseManagement.Application.Commands.ShipmentDocuments;

namespace WarehouseManagement.Application.Validators.ShipmentDocuments;

/// <summary>
/// Валидатор для команды обновления документа отгрузки
/// </summary>
public class UpdateShipmentDocumentCommandValidator : AbstractValidator<UpdateShipmentDocumentCommand>
{
    public UpdateShipmentDocumentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор документа должен быть положительным");

        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Номер документа обязателен")
            .MaximumLength(50)
            .WithMessage("Номер документа не может превышать 50 символов");

        RuleFor(x => x.ClientId)
            .GreaterThan(0)
            .WithMessage("Идентификатор клиента должен быть положительным");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Дата документа обязательна")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Дата документа не может быть в будущем");

        RuleFor(x => x.Resources)
            .NotNull()
            .WithMessage("Список ресурсов не может быть null")
            .NotEmpty()
            .WithMessage("Документ отгрузки не может быть пустым");

        RuleForEach(x => x.Resources)
            .SetValidator(new UpdateShipmentResourceDtoValidator());
    }
}

/// <summary>
/// Валидатор для DTO обновления ресурса в документе отгрузки
/// </summary>
public class UpdateShipmentResourceDtoValidator : AbstractValidator<UpdateShipmentResourceDto>
{
    public UpdateShipmentResourceDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Идентификатор записи ресурса не может быть отрицательным");

        RuleFor(x => x.ResourceId)
            .GreaterThan(0)
            .WithMessage("Идентификатор ресурса должен быть положительным");

        RuleFor(x => x.UnitOfMeasureId)
            .GreaterThan(0)
            .WithMessage("Идентификатор единицы измерения должен быть положительным");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Количество должно быть положительным");
    }
}