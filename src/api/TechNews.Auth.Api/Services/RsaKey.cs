namespace TechNews.Auth.Api.Services;

public class RsaKey
{
    public RsaKey(System.Security.Cryptography.RSA instance)
    {
        CreationDate = DateTime.UtcNow;
        Instance = instance;
    }
    
    public DateTime CreationDate { get; set; }
    public System.Security.Cryptography.RSA Instance  { get; set; }

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
