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
                services.AddScoped<ICryptographicKeyFactory, EcdsaCryptographicKeyFactory>();
                services.AddScoped<ICryptographicKey, EcdsaCryptographicKey>();
                break;
            case "RSA":
            default:
                services.AddScoped<ICryptographicKeyFactory, RsaCryptographicKeyFactory>();
                services.AddScoped<ICryptographicKey, RsaCryptographicKey>();
                break;
        }

        if (string.IsNullOrWhiteSpace(EnvironmentVariables.AzureKeyVaultUrl))
        {
            throw new ApplicationException($"Environment variable '{nameof(EnvironmentVariables.AzureKeyVaultUrl)}' is not defined. " +
                                           $"Please consider using service '{nameof(CryptographicKeyInMemoryRetriever)}'.");
        }

        services.AddSingleton<ICryptographicKeyRetriever, CryptographicKeyAzureVaultRetriever>();

        services.AddHostedService<KeyRotatorBackgroundService>();

        return services;
    }
}