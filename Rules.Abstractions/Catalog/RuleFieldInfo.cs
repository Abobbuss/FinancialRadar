namespace Rules.Abstractions.Catalog;

/// <summary>
/// Поле, доступное в правилах (например: amount, currency, ts).
/// </summary>
public sealed record RuleFieldInfo( 
    string Name,                        // Имя правила
    DataTypeInfo DataTypeCode,          // Тип данных, с которым работает это поле
    int? ValueDomainId,                 // Если enum - ссылка на домейн
    IReadOnlyList<string> Operators     // Коды операторов, с которым он может взаимодействовать
);