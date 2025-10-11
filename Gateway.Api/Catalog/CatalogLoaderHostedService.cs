namespace Gateway.Api.Catalog;

/// <summary>
/// Хост для отправки снепшота в движок
/// </summary>
public sealed class CatalogLoaderHostedService(
    IServiceProvider sp,
    CatalogProvider memory,
    ILogger<CatalogLoaderHostedService> logger
) : IHostedService
{
    private long _version = 0;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await ReloadAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public async Task ReloadAsync(CancellationToken ct = default)
    {
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<Persistence.AppDbContext>();

        var snapshot = await CatalogBuilder.BuildAsync(db, Interlocked.Increment(ref _version));
        memory.Replace(snapshot);

        logger.LogInformation("Catalog loaded. Version={Version}, Fields={Fields}, Ops={Ops}, Domains={Domains}, Consts={Consts}",
            snapshot.Version, snapshot.Fields.Count, snapshot.Operators.Count, snapshot.Domains.Count, snapshot.Constants.Count);
    }
}