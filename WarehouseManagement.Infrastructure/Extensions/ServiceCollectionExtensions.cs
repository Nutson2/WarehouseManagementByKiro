using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.Domain.Interfaces;
using WarehouseManagement.Infrastructure.Data;
using WarehouseManagement.Infrastructure.Repositories;

namespace WarehouseManagement.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Get database provider from configuration
        var databaseProvider = configuration["DatabaseProvider"] ?? "SqlServer";
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configure Entity Framework based on provider
        switch (databaseProvider.ToLower())
        {
            case "postgresql":
                services.AddDbContext<WarehouseDbContext>(options =>
                    options.UseNpgsql(connectionString));
                break;
            case "sqlserver":
            default:
                services.AddDbContext<WarehouseDbContext>(options =>
                    options.UseSqlServer(connectionString));
                break;
        }

        // Register repositories
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IBalanceRepository, BalanceRepository>();
        services.AddScoped<IReceiptDocumentRepository, ReceiptDocumentRepository>();
        services.AddScoped<IShipmentDocumentRepository, ShipmentDocumentRepository>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}