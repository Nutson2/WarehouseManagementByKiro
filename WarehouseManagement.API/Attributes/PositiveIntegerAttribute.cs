using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.API.Attributes;

/// <summary>
/// Атрибут валидации для положительных целых чисел
/// </summary>
public class PositiveIntegerAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return true; // Null values are handled by Required attribute

        if (value is int intValue)
            return intValue > 0;

        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"Поле {name} должно быть положительным числом";
    }
}