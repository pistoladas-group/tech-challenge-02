using Microsoft.EntityFrameworkCore;
using TechNews.Auth.Api.Data;

namespace TechNews.Auth.Api.Configuration;

public static class Database
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), assembly => // TODO: Pegar do .env
                assembly.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)
                        .EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(2), errorNumbersToAdd: null)
                        .MigrationsHistoryTable("_AppliedMigrations"));
        });
    }
}