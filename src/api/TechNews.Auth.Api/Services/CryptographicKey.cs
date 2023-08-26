using TechNews.Auth.Api.Configurations;

namespace TechNews.Auth.Api.Services;

public class CryptographicKey
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public System.Security.Cryptography.RSA Instance { get; set; }

    public CryptographicKey(System.Security.Cryptography.RSA instance)
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
        Instance = instance;
    }

    public bool IsValid()
    {
        var today = DateTime.UtcNow;

        if (Instance is null)
        {
            return false;
        }

        return (today - CreationDate).TotalDays < EnvironmentVariables.KeyExpirationInDays;
    }
}
