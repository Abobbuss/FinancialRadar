using Rules.Abstractions.Catalog;

namespace Rules.Engine.Catalog;

/// <summary>
/// Иммутабельный «снимок» каталога, который использует движок:
/// поля, операторы, домены, константы. 
/// </summary>
public sealed record CatalogView(
    IReadOnlyList<RuleFieldInfo> Fields,
    IReadOnlyList<RuleOperatorInfo> Operators,
    IReadOnlyList<ValueDomainInfo> Domains,
    IReadOnlyList<PolicyConstantInfo> Constants,
    long Version,                // монотонный номер/ETag снапшота
    DateTime LoadedAtUtc         // когда был собран снимок
);