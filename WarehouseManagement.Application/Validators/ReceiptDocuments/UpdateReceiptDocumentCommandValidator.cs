using FluentValidation;
using WarehouseManagement.Application.Commands.ReceiptDocuments;

namespace WarehouseManagement.Application.Validators.ReceiptDocuments;

/// <summary>
/// Валидатор для команды обновления документа поступления
/// </summary>
public class UpdateReceiptDocumentCommandValidator : AbstractValidator<UpdateReceiptDocumentCommand>
{
    public UpdateReceiptDocumentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор документа должен быть положительным");

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
            .SetValidator(new UpdateReceiptResourceDtoValidator());
    }
}

/// <summary>
/// Валидатор для DTO обновления ресурса в документе поступления
/// </summary>
public class UpdateReceiptResourceDtoValidator : AbstractValidator<UpdateReceiptResourceDto>
{
    public UpdateReceiptResourceDtoValidator()
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