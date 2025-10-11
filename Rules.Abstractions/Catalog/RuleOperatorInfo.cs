namespace Rules.Abstractions.Catalog;

/// <summary>
/// Оператор, который может применяться к полю (==, >, in, between, hour_ge, ...).
/// </summary>
public sealed record RuleOperatorInfo( 
    string Code     // Машинное название оператора
    );