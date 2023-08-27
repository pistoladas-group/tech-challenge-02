using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using TechNews.Auth.Api.Configurations;
using TechNews.Auth.Api.Models;

namespace TechNews.Auth.Api.Services.Cryptography;

public class EcdsaCryptographicKey : ICryptographicKey
{
    public DateTime? CreationDate { get; private set; }

    private Guid? _id { get; set; }
    private ECDsa? _keyInstance { get; set; }

    public ICryptographicKey CreateKey()
    {
        _id = Guid.NewGuid();
        _keyInstance = ECDsa.Create(ECCurve.NamedCurves.nistP521);

        CreationDate = DateTime.UtcNow;

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

        return new SigningCredentials(new ECDsaSecurityKey(_keyInstance), SecurityAlgorithms.EcdsaSha512)
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
            KeyType = "EC",
            KeyId = _id.ToString(),
            Algorithm = "ES512",
            Use = "sig",
            XAxis = Convert.ToBase64String(publicParameters.Q.X ?? Array.Empty<byte>()),
            YAxis = Convert.ToBase64String(publicParameters.Q.Y ?? Array.Empty<byte>()),
            Curve = "P-512"
        };
    }
}