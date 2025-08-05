using FluentValidation;
using WarehouseManagement.Application.Commands.ShipmentDocuments;

namespace WarehouseManagement.Application.Validators.ShipmentDocuments;

/// <summary>
/// Валидатор для команды удаления документа отгрузки
/// </summary>
public class DeleteShipmentDocumentCommandValidator : AbstractValidator<DeleteShipmentDocumentCommand>
{
    public DeleteShipmentDocumentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор документа должен быть положительным");
    }
}