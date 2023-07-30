namespace TechNews.Auth.Api.Services;

public class CryptographicKeyInMemoryRetriever : ICryptographicKeyRetriever
{
    private IList<CryptographicKey> _keys { get; set; } = new List<CryptographicKey>();

    public CryptographicKey? GetExistingKey()
    {
        return _keys.OrderByDescending(k => k.CreationDate).FirstOrDefault();
    }

    public void StoreKey(CryptographicKey key)
    {
        _keys.Add(key);
    }

    public IList<CryptographicKey> GetLastKeys(int quantity)
    {
        return _keys.OrderByDescending(k => k.CreationDate).Take(quantity).ToList();
    }
}