using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Application.DTOs;

/// <summary>
/// DTO для ресурса
/// </summary>
public class ResourceDto
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Статус ресурса
    /// </summary>
    public EntityStatus Status { get; set; }
}

/// <summary>
/// DTO для создания ресурса
/// </summary>
public class CreateResourceDto
{
    /// <summary>
    /// Наименование ресурса
    /// </summary>
    [Required(ErrorMessage = "Наименование ресурса не может быть пустым")]
    [StringLength(255, ErrorMessage = "Наименование ресурса не может превышать 255 символов")]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// DTO для обновления ресурса
/// </summary>
public class UpdateResourceDto
{
    /// <summary>
    /// Наименование ресурса
    /// </summary>
    [Required(ErrorMessage = "Наименование ресурса не может быть пустым")]
    [StringLength(255, ErrorMessage = "Наименование ресурса не может превышать 255 символов")]
    public string Name { get; set; } = string.Empty;
}