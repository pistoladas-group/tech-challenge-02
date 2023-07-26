namespace TechNews.Auth.Api.Services;

public class RsaKeyMockRetriever : IRsaKeyRetriever
{
    private RsaKey? _RSA;

    public RsaKey? GetExistingKey()
    {
        return _RSA;
    }

    public void StoreKey(RsaKey key)
    {
        _RSA = key;
    }
}