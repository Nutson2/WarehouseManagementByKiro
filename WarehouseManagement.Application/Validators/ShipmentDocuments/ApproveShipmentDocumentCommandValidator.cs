using FluentValidation;
using WarehouseManagement.Application.Commands.ShipmentDocuments;

namespace WarehouseManagement.Application.Validators.ShipmentDocuments;

/// <summary>
/// Валидатор для команды подписания документа отгрузки
/// </summary>
public class ApproveShipmentDocumentCommandValidator : AbstractValidator<ApproveShipmentDocumentCommand>
{
    public ApproveShipmentDocumentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Идентификатор документа должен быть положительным");
    }
}