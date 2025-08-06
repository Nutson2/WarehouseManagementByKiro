using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.API.Attributes;

/// <summary>
/// Атрибут валидации для положительных десятичных чисел
/// </summary>
public class PositiveDecimalAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return true; // Null values are handled by Required attribute

        if (value is decimal decimalValue)
            return decimalValue > 0;

        if (value is double doubleValue)
            return doubleValue > 0;

        if (value is float floatValue)
            return floatValue > 0;

        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"Поле {name} должно быть положительным числом";
    }
}