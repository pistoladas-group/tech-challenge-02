using Microsoft.EntityFrameworkCore;
using TechNews.Auth.Api.Data;

namespace TechNews.Auth.Api.Configurations;

public static class Database
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariables.DatabaseConnectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ApplicationException("Undefined Database Connection String. Please check the Environment Variables.");
        }

        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlServer(connectionString, assembly =>
                assembly.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)
                        .EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(2), errorNumbersToAdd: null)
                        .MigrationsHistoryTable("_AppliedMigrations"));
        });

        return services;
    }
}