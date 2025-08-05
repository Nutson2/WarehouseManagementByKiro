using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.Infrastructure.Data;

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

        return services;
    }
}