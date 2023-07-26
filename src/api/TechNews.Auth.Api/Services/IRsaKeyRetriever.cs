namespace TechNews.Auth.Api.Services;

public interface IRsaKeyRetriever
{
    public RsaKey? GetExistingKey();
    public void StoreKey(RsaKey key);
}