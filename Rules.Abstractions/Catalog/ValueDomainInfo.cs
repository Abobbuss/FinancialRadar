namespace Rules.Abstractions.Catalog;

/// <summary>
/// Домен перечислимых значений (например currencies: RUB, USD, EUR).
/// </summary>
public sealed record ValueDomainInfo( 
    int Id,                         // Его Id, как в БД
    string Code,                    // Машинное имя
    IReadOnlyList<string> Values,   // Список данных
    DataTypeInfo DataType           // Тип данных у этого энама
);