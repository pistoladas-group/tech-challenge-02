using TechNews.Auth.Api.Services.Crypto.RSA;

namespace TechNews.Auth.Api.Services.Crypto;

public class AssymetricKey
{
    public AssymetricKey(Guid keyId, byte[] data, RsaPublicParameters publicParameters)
    {
        KeyId = keyId;
        CreationDate = DateTime.UtcNow;
        Data = data;
        PublicParameters = publicParameters;
    }

    public Guid KeyId { get; set; }
    public DateTime CreationDate { get; set; }
    public byte[] Data { get; set; }
    public RsaPublicParameters PublicParameters { get; set; }
}
