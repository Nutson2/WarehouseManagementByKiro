using FluentValidation;
using WarehouseManagement.Application.Commands.ReceiptDocuments;

namespace WarehouseManagement.Application.Validators.ReceiptDocuments;

/// <summary>
/// Валидатор для команды удаления документа поступления
/// </summary>
public class DeleteReceiptDocumentCommandValidator : AbstractValidator<DeleteReceiptDocumentCommand>
{
    public DeleteReceiptDocumentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор документа должен быть положительным");
    }
}