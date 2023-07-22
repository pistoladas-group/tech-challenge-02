namespace TechNews.Auth.Api.Services.Crypto.RSA;

public class RsaCrypto
{
    public AssymetricKey CreateKeys()
    {
        var key = System.Security.Cryptography.RSA.Create(2048);
        var keysData = key.ExportRSAPrivateKey();
        var keyId = Guid.NewGuid();

        return new AssymetricKey(keyId, keysData, new RsaPublicParameters(key, keyId));
    }
}
