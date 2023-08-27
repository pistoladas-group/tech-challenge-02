namespace TechNews.Auth.Api.Services.KeyRetrievers;

public interface ICryptographicKeyRetriever
{
    ICryptographicKey? GetExistingKey();
    void StoreKey(ICryptographicKey key);
    IList<ICryptographicKey>? GetLastKeys(int quantity);
}