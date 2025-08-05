namespace WarehouseManagement.Domain.Enums;

/// <summary>
/// Статус документа в системе
/// </summary>
public enum DocumentStatus
{
    /// <summary>
    /// Черновик документа
    /// </summary>
    Draft = 1,
    
    /// <summary>
    /// Подписанный документ
    /// </summary>
    Approved = 2
}