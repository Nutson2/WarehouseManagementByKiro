using System.Net;
using System.Text.Json;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.API.Middleware;

/// <summary>
/// Middleware для глобальной обработки исключений
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла необработанная ошибка: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                code = GetErrorCode(exception),
                message = GetErrorMessage(exception),
                details = GetErrorDetails(exception)
            }
        };

        context.Response.StatusCode = GetStatusCode(exception);

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }

    private static int GetStatusCode(Exception exception)
    {
        if (exception is EntityNotFoundException)
            return (int)HttpStatusCode.NotFound;
        
        if (exception is DuplicateEntityException || exception is InvalidEntityStatusException)
            return (int)HttpStatusCode.BadRequest;
        
        if (exception is EntityInUseException || exception is InsufficientBalanceException)
            return (int)HttpStatusCode.UnprocessableEntity;
        
        if (exception is ArgumentException || exception is ArgumentNullException)
            return (int)HttpStatusCode.BadRequest;
        
        return (int)HttpStatusCode.InternalServerError;
    }

    private static string GetErrorCode(Exception exception)
    {
        if (exception is EntityNotFoundException)
            return "ENTITY_NOT_FOUND";
        
        if (exception is DuplicateEntityException)
            return "DUPLICATE_ENTITY";
        
        if (exception is InvalidEntityStatusException)
            return "INVALID_STATUS";
        
        if (exception is EntityInUseException)
            return "ENTITY_IN_USE";
        
        if (exception is InsufficientBalanceException)
            return "INSUFFICIENT_BALANCE";
        
        if (exception is ArgumentException || exception is ArgumentNullException)
            return "INVALID_ARGUMENT";
        
        return "INTERNAL_ERROR";
    }

    private static string GetErrorMessage(Exception exception)
    {
        if (exception is DomainException || exception is ArgumentException || exception is ArgumentNullException)
            return exception.Message;
        
        return "Произошла внутренняя ошибка сервера";
    }

    private static object? GetErrorDetails(Exception exception)
    {
        if (exception is InsufficientBalanceException balanceEx)
        {
            return new
            {
                resourceId = balanceEx.ResourceId,
                unitOfMeasureId = balanceEx.UnitOfMeasureId,
                required = balanceEx.RequiredQuantity,
                available = balanceEx.AvailableQuantity
            };
        }
        
        return null;
    }
}