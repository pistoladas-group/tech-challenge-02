using TechNews.Auth.Api.Services;

namespace TechNews.Auth.Api.Configurations;

public static class DependencyInjections
{
    public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped<RsaTokenSigner>();
        services.AddSingleton<IRsaKeyRetriever, RsaKeyMockRetriever>();

        services.AddHostedService<KeyRotatorBackgroundService>();

        return services;
    }
}