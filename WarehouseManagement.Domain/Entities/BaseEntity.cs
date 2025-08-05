using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Domain.Entities;

/// <summary>
/// Базовая сущность для всех доменных объектов
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Статус сущности (активная/архивная)
    /// </summary>
    public EntityStatus Status { get; set; } = EntityStatus.Active;
}