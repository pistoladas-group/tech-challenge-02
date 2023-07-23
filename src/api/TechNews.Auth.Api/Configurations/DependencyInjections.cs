using TechNews.Auth.Api.Services.Crypto.RSA;

namespace TechNews.Auth.Api.Configurations;

public static class DependencyInjections
{
    public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped<RsaCrypto>();

        return services;
    }
}