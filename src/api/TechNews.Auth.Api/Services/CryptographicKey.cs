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

        if (Instance is null) //TODO: verificar forma de validar se instância é valida
        {
            return false;
        }

        return (today - CreationDate).TotalDays < 30; //TODO: configurar e validar
    }
}
