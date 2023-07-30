using Microsoft.IdentityModel.Tokens;

namespace TechNews.Auth.Api.Services;

public class RsaTokenSigner
{
    private readonly ICryptographicKeyRetriever _cryptographicKeyRetriever;
    public RsaTokenSigner(ICryptographicKeyRetriever rsaKeyRetriever)
    {
        _cryptographicKeyRetriever = rsaKeyRetriever;
    }

    public SigningCredentials? GetSigningCredentials()
    {
        var key = _cryptographicKeyRetriever.GetExistingKey();

        if (key is null)
        {
            return null;
        }

        return new SigningCredentials(new RsaSecurityKey(key.Instance), SecurityAlgorithms.RsaSha256)
        {
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };
    }
}