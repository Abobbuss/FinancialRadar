namespace Rules.Engine.Catalog;

/// <summary>
/// Нотификатор о смене снапшота каталога.
/// Полезно, если какие-то части движка хотят реагировать на обновление.
/// </summary>
public interface ICatalogUpdateNotifier
{
    /// <summary>
    /// Текущее значение версии (ETag) каталога.
    /// </summary>
    long CurrentVersion { get; }

    /// <summary>
    /// Подписка на событие обновления каталога. Вызывается после атомарной замены снапшота.
    /// </summary>
    event Action<long>? OnCatalogUpdated;
}