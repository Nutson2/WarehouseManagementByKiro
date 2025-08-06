using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WarehouseManagement.API.Models;

namespace WarehouseManagement.API.Attributes;

/// <summary>
/// Атрибут для валидации модели на уровне API
/// </summary>
public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var response = new ValidationErrorResponse
            {
                Error = new ValidationErrorInfo
                {
                    Details = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                }
            };

            context.Result = new BadRequestObjectResult(response);
        }
    }
}