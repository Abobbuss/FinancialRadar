namespace Rules.Engine.Catalog;

/// <summary>
/// Провайдер актуального снимка каталога для Rule Engine.
/// </summary>
public interface ICatalogProvider
{
    /// <summary>
    /// Получить текущий снимок каталога.
    /// Реализация должна возвращать иммутабельный объект.
    /// </summary>
    CatalogView GetCurrent();

    /// <summary>
    /// Асинхронно получить текущий снимок (если нужно валидации).
    /// По умолчанию может просто оборачивать GetCurrent().
    /// </summary>
    Task<CatalogView> GetCurrentAsync(CancellationToken ct = default);
}