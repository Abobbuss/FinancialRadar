namespace Rules.Abstractions.Catalog;

/// <summary>
/// Источник каталога для валидатора/компилятора правил.
/// Реализация предоставляет перечни полей, операторов, доменов и констант.
/// </summary>
public interface IRuleCatalog
{
    /// <summary>
    /// Получить поля для создания правил.
    /// </summary>
    Task<IReadOnlyList<RuleFieldInfo>> GetFieldsAsync(CancellationToken ct = default);
    
    /// <summary>
    /// Получить булевые операторы.
    /// </summary>
    Task<IReadOnlyList<RuleOperatorInfo>> GetOperatorsAsync(CancellationToken ct = default);
    
    /// <summary>
    /// Получить энамы.
    /// </summary>
    Task<IReadOnlyList<ValueDomainInfo>> GetDomainsAsync(CancellationToken ct = default);
    
    /// <summary>
    /// Получить константы.
    /// </summary>
    Task<IReadOnlyList<PolicyConstantInfo>> GetConstantsAsync(CancellationToken ct = default);
}