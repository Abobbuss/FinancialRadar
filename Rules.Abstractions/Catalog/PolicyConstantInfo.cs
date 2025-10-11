namespace Rules.Abstractions.Catalog;

/// <summary>
/// Именованные константы для DSL (например LIMIT_NO_PIN), со сведениями о типе и сериализованном значении.
/// </summary>
public sealed record PolicyConstantInfo( 
    string Code,                // Машинное описание
    DataTypeInfo DataTypeCode,  // Тип передаваемого значения
    string Value                // Значение
);