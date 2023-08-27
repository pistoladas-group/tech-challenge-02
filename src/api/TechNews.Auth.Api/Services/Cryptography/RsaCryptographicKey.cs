using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using TechNews.Auth.Api.Configurations;
using TechNews.Auth.Api.Models;

public class RsaCryptographicKey : ICryptographicKey
{
    public DateTime? CreationDate { get; private set; }
    private Guid? _id { get; set; }
    private RSA? _keyInstance { get; set; }

    public ICryptographicKey CreateKey()
    {
        _id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
        _keyInstance = RSA.Create(EnvironmentVariables.KeyCreationSizeInBits);

        return this;
    }

    public bool IsValid()
    {
        if (_keyInstance is null)
        {
            return false;
        }

        return (DateTime.UtcNow - CreationDate)?.TotalDays < EnvironmentVariables.KeyExpirationInDays;
    }

    public SigningCredentials? GetSigningCredentials()
    {
        if (_keyInstance is null)
        {
            return null;
        }

        return new SigningCredentials(new RsaSecurityKey(_keyInstance), SecurityAlgorithms.RsaSha256)
        {
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };
    }

    public JsonWebKeyModel? GetJsonWebKey()
    {
        if (_keyInstance is null)
        {
            return null;
        }

        var publicParameters = _keyInstance.ExportParameters(false);

        return new JsonWebKeyModel()
        {
            KeyType = "RSA",
            KeyId = _id.ToString(),
            Algorithm = "RS256",
            Use = "sig",
            Modulus = Base64UrlEncoder.Encode(publicParameters.Modulus),
            Exponent = Base64UrlEncoder.Encode(publicParameters.Exponent)
        };
    }
}