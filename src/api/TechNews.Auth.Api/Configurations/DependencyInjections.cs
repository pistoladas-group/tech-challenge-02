using TechNews.Auth.Api.Services;
using TechNews.Auth.Api.Services.Cryptography;
using TechNews.Auth.Api.Services.KeyRetrievers;

namespace TechNews.Auth.Api.Configurations;

public static class DependencyInjections
{
    public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
    {
        switch (EnvironmentVariables.CryptographicAlgorithm)
        {
            case "ECC":
                services.AddScoped<ICryptographicKey, EcdsaCryptographicKey>();
                break;
            case "RSA":
            default:
                services.AddScoped<ICryptographicKey, RsaCryptographicKey>();
                break;
        }

        services.AddSingleton<ICryptographicKeyRetriever, CryptographicKeyInMemoryRetriever>();

        services.AddHostedService<KeyRotatorBackgroundService>();

        return services;
    }
}