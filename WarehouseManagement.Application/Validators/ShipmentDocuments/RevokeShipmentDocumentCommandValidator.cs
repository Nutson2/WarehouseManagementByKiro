using FluentValidation;
using WarehouseManagement.Application.Commands.ShipmentDocuments;

namespace WarehouseManagement.Application.Validators.ShipmentDocuments;

/// <summary>
/// Валидатор для команды отзыва документа отгрузки
/// </summary>
public class RevokeShipmentDocumentCommandValidator : AbstractValidator<RevokeShipmentDocumentCommand>
{
    public RevokeShipmentDocumentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор документа должен быть положительным");
    }
}