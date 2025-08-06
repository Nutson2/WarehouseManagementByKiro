using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WarehouseManagement.Application.Behaviors;

/// <summary>
/// Поведение для логирования операций через MediatR pipeline
/// </summary>
/// <typeparam name="TRequest">Тип запроса</typeparam>
/// <typeparam name="TResponse">Тип ответа</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("Начало выполнения операции {RequestName}", requestName);

        try
        {
            var response = await next();
            
            stopwatch.Stop();
            _logger.LogInformation("Операция {RequestName} выполнена успешно за {ElapsedMilliseconds}ms", 
                requestName, stopwatch.ElapsedMilliseconds);
            
            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Ошибка при выполнении операции {RequestName} за {ElapsedMilliseconds}ms: {ErrorMessage}", 
                requestName, stopwatch.ElapsedMilliseconds, ex.Message);
            
            throw;
        }
    }
}