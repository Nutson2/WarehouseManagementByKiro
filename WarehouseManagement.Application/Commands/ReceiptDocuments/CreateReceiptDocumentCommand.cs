using MediatR;
using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Commands.ReceiptDocuments;

/// <summary>
/// Команда для создания документа поступления
/// </summary>
public class CreateReceiptDocumentCommand : IRequest<ReceiptDocumentDto>
{
    /// <summary>
    /// Номер документа
    /// </summary>
    [Required(ErrorMessage = "Номер документа обязателен")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Номер документа должен содержать от 1 до 50 символов")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Дата документа
    /// </summary>
    [Required(ErrorMessage = "Дата документа обязательна")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Ресурсы в документе поступления
    /// </summary>
    [Required(ErrorMessage = "Список ресурсов обязателен")]
    public List<CreateReceiptResourceDto> Resources { get; set; } = new();
}

/// <summary>
/// DTO для создания ресурса в документе поступления
/// </summary>
public class CreateReceiptResourceDto
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор ресурса должен быть положительным")]
    public int ResourceId { get; set; }

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор единицы измерения должен быть положительным")]
    public int UnitOfMeasureId { get; set; }

    /// <summary>
    /// Количество ресурса
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "Количество должно быть положительным")]
    public decimal Quantity { get; set; }
}