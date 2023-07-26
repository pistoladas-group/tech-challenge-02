using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace TechNews.Auth.Api.Models;

public class RsaPublicParameters
{
    public RsaPublicParameters(System.Security.Cryptography.RSA rsa, Guid keyId)
    {
        var parameters = rsa.ExportParameters(false);

        KeyType = "RSA";
        KeyId = keyId.ToString();
        Algorithm = "RS256";
        Use = "sig";
        Modulus = Base64UrlEncoder.Encode(parameters.Modulus);
        Exponent = Base64UrlEncoder.Encode(parameters.Exponent);
    }

    public string KeyType { get; set; }
    public string KeyId { get; set; }
    public string Algorithm { get; set; }
    public string Use { get; set; }
    public string Modulus { get; set; }
    public string Exponent { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
