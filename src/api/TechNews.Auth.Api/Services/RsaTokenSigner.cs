using Microsoft.IdentityModel.Tokens;

namespace TechNews.Auth.Api.Services;

public class RsaTokenSigner
{
    private readonly IRsaKeyRetriever _rsaKeyRetriever;
    public RsaTokenSigner(IRsaKeyRetriever rsaKeyRetriever)
    {
        _rsaKeyRetriever = rsaKeyRetriever;
    }

    public SigningCredentials GetSigningCredentials() 
    {
        var teste = _rsaKeyRetriever.GetExistingKey();

        return new SigningCredentials(new RsaSecurityKey(teste.Instance), SecurityAlgorithms.RsaSha256)
        {
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };
    }
}