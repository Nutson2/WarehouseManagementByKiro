namespace WarehouseManagement.API.Models;

/// <summary>
/// Стандартизированный ответ об ошибке
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Информация об ошибке
    /// </summary>
    public ErrorInfo Error { get; set; } = null!;
}

/// <summary>
/// Информация об ошибке
/// </summary>
public class ErrorInfo
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Дополнительные детали ошибки
    /// </summary>
    public object? Details { get; set; }
}

/// <summary>
/// Ответ с ошибками валидации
/// </summary>
public class ValidationErrorResponse
{
    /// <summary>
    /// Информация об ошибке валидации
    /// </summary>
    public ValidationErrorInfo Error { get; set; } = null!;
}

/// <summary>
/// Информация об ошибке валидации
/// </summary>
public class ValidationErrorInfo
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public string Code { get; set; } = "VALIDATION_ERROR";

    /// <summary>
    /// Общее сообщение об ошибке
    /// </summary>
    public string Message { get; set; } = "Ошибка валидации данных";

    /// <summary>
    /// Детали ошибок валидации по полям
    /// </summary>
    public Dictionary<string, string[]> Details { get; set; } = new();
}