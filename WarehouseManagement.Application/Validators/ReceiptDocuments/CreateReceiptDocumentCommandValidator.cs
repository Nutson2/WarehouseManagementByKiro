using FluentValidation;
using WarehouseManagement.Application.Commands.ReceiptDocuments;

namespace WarehouseManagement.Application.Validators.ReceiptDocuments;

/// <summary>
/// Валидатор для команды создания документа поступления
/// </summary>
public class CreateReceiptDocumentCommandValidator : AbstractValidator<CreateReceiptDocumentCommand>
{
    public CreateReceiptDocumentCommandValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Номер документа обязателен")
            .MaximumLength(50)
            .WithMessage("Номер документа не может превышать 50 символов");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Дата документа обязательна")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Дата документа не может быть в будущем");

        RuleFor(x => x.Resources)
            .NotNull()
            .WithMessage("Список ресурсов не может быть null");

        RuleForEach(x => x.Resources)
            .SetValidator(new CreateReceiptResourceDtoValidator());
    }
}

/// <summary>
/// Валидатор для DTO создания ресурса в документе поступления
/// </summary>
public class CreateReceiptResourceDtoValidator : AbstractValidator<CreateReceiptResourceDto>
{
    public CreateReceiptResourceDtoValidator()
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