using FluentValidation;
using WarehouseManagement.Application.Commands.ShipmentDocuments;

namespace WarehouseManagement.Application.Validators.ShipmentDocuments;

/// <summary>
/// Валидатор для команды создания документа отгрузки
/// </summary>
public class CreateShipmentDocumentCommandValidator : AbstractValidator<CreateShipmentDocumentCommand>
{
    public CreateShipmentDocumentCommandValidator()
    {
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
            .SetValidator(new CreateShipmentResourceDtoValidator());
    }
}

/// <summary>
/// Валидатор для DTO создания ресурса в документе отгрузки
/// </summary>
public class CreateShipmentResourceDtoValidator : AbstractValidator<CreateShipmentResourceDto>
{
    public CreateShipmentResourceDtoValidator()
    {
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