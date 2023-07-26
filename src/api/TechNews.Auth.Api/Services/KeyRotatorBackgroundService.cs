using Serilog;

namespace TechNews.Auth.Api.Services;

public class KeyRotatorBackgroundService : IHostedService, IDisposable
{
    private const string ServiceName = "Key Rotator";
    private Timer? _timer;
    private bool _isProcessing;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public KeyRotatorBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(ExecuteProcessAsync, null, TimeSpan.Zero, TimeSpan.FromMinutes(5)); //TODO: deixar configurável
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("Background service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private async void ExecuteProcessAsync(object? state)
    {
        if (_isProcessing)
        {
            Log.Debug("{service}: last execution still running. Aborting.", ServiceName);
            return;
        }

        _isProcessing = true;
        Log.Debug("{service}: started.", ServiceName);

        using var scope = _serviceScopeFactory.CreateScope();
        var keyRetrieverService = scope.ServiceProvider.GetRequiredService<IRsaKeyRetriever>();

        var existingKey = keyRetrieverService.GetExistingKey();
        var isKeyValid = existingKey?.IsValid() ?? false;

        if (!isKeyValid)
        {
            existingKey = CreateKey();
            keyRetrieverService.StoreKey(existingKey);
        }

        if (existingKey is null)
        {
            Log.Error("{service}: An error occurred when trying to get the RSA key.", ServiceName);
            throw new ApplicationException("An error occurred when trying to get the RSA key");
        }

        _isProcessing = false;
        Log.Debug("{service}: finished.", ServiceName);
    }

    public RsaKey CreateKey()
    {
        var rsa = System.Security.Cryptography.RSA.Create(2048); //TODO: configurar
        var rsaKey = new RsaKey(rsa);

        return rsaKey;
    }
}
