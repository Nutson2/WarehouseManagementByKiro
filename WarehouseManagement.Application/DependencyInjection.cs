using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WarehouseManagement.Application.Behaviors;

namespace WarehouseManagement.Application;

/// <summary>
/// Расширение для регистрации сервисов Application слоя
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавить сервисы Application слоя
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Регистрируем AutoMapper
        services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

        // Регистрируем MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Регистрируем FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Регистрируем поведения MediatR pipeline
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}