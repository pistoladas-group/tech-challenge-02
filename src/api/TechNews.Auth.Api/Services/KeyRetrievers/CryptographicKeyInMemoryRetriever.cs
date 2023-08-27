namespace TechNews.Auth.Api.Services.KeyRetrievers;

public class CryptographicKeyInMemoryRetriever : ICryptographicKeyRetriever
{
    private IList<ICryptographicKey> _keys { get; set; } = new List<ICryptographicKey>();

    public ICryptographicKey? GetExistingKey()
    {
        return _keys.OrderByDescending(k => k.CreationDate).FirstOrDefault();
    }

    public void StoreKey(ICryptographicKey key)
    {
        _keys.Add(key);
    }

    public IList<ICryptographicKey> GetLastKeys(int quantity)
    {
        return _keys.OrderByDescending(k => k.CreationDate).Take(quantity).ToList();
    }
}