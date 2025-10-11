using Rules.Abstractions.Catalog;

namespace Gateway.Api.Catalog;

/// <summary>
/// Снимок каталога, который передается в движок
/// </summary>
/// <param name="Fields">Поля</param>
/// <param name="Operators">Операторы</param>
/// <param name="Domains">Энамы</param>
/// <param name="Constants">Константы</param>
/// <param name="Version">Версия</param>
/// <param name="LoadedAtUtc">Дата загрузки</param>
public sealed record CatalogSnapshot(
    IReadOnlyList<RuleFieldInfo> Fields,
    IReadOnlyList<RuleOperatorInfo> Operators,
    IReadOnlyList<ValueDomainInfo> Domains,
    IReadOnlyList<PolicyConstantInfo> Constants,
    long Version,
    DateTime LoadedAtUtc
);