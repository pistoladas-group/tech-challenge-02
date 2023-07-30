namespace TechNews.Auth.Api.Services;

public interface ICryptographicKeyRetriever
{
    CryptographicKey? GetExistingKey();
    void StoreKey(CryptographicKey key);
    IList<CryptographicKey>? GetLastKeys(int quantity);
}