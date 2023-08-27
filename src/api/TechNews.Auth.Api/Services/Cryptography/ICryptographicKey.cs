using Microsoft.IdentityModel.Tokens;
using TechNews.Auth.Api.Models;

public interface ICryptographicKey
{
    DateTime? CreationDate { get; }

    ICryptographicKey CreateKey();
    bool IsValid();
    SigningCredentials? GetSigningCredentials();
    JsonWebKeyModel? GetJsonWebKey();
}