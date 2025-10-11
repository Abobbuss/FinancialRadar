using Rules.Engine.Catalog;

namespace Gateway.Api.Catalog;

/// <summary>
/// Провайдер передачи актуальных данных в движок
/// </summary>
public sealed class CatalogProvider : ICatalogProvider, ICatalogUpdateNotifier
{
    private volatile CatalogSnapshot _current = new(
        [], [], [], [], Version:0, LoadedAtUtc: DateTime.MinValue);

    public CatalogSnapshot Current => _current;
    
    public CatalogView GetCurrent()
        => new(_current.Fields, _current.Operators, _current.Domains, _current.Constants,
            _current.Version, _current.LoadedAtUtc);

    public Task<CatalogView> GetCurrentAsync(CancellationToken ct = default)
        => Task.FromResult(GetCurrent());

    public long CurrentVersion => _current.Version;

    public event Action<long>? OnCatalogUpdated;

    public void Replace(CatalogSnapshot snapshot)
    {
        _current = snapshot;
        OnCatalogUpdated?.Invoke(snapshot.Version);
    }
}