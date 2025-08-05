using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Application.DTOs;

/// <summary>
/// DTO для клиента
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование клиента
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Адрес клиента
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Статус клиента
    /// </summary>
    public EntityStatus Status { get; set; }
}

/// <summary>
/// DTO для создания клиента
/// </summary>
public class CreateClientDto
{
    /// <summary>
    /// Наименование клиента
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Адрес клиента
    /// </summary>
    public string Address { get; set; } = string.Empty;
}

/// <summary>
/// DTO для обновления клиента
/// </summary>
public class UpdateClientDto
{
    /// <summary>
    /// Наименование клиента
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Адрес клиента
    /// </summary>
    public string Address { get; set; } = string.Empty;
}