using Microsoft.EntityFrameworkCore;
using TechNews.Core.Api.Data;

namespace TechNews.Core.Api.Configurations;

public static class Database
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        var connectionString = EnvironmentVariables.DatabaseConnectionString;

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ApplicationException("Undefined Database Connection String. Please check the Environment Variables.");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, assembly =>
                assembly.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                        .EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(2), errorNumbersToAdd: null)
                        .MigrationsHistoryTable("_AppliedMigrations"));
        });

        return services;
    }
}