namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Таблица отображающая связь поле и оператора
/// </summary>
public class RuleFieldOperator
{
    /// <summary>
    /// Fk на RuleField
    /// </summary>
    public int FieldId { get; set; }
    
    /// <summary>
    /// Fk на RuleField
    /// </summary>
    public RuleField Field { get; set; } = default!;
    
    /// <summary>
    /// Fk на RuleOperator
    /// </summary>
    public int OperatorId { get; set; }
    
    /// <summary>
    /// Fk на RuleOperator
    /// </summary>
    public RuleOperator Operator { get; set; } = default!;
    
    /// <summary>
    /// Какое-то ограничение, если такое есть (например min 0, max 99)
    /// </summary>
    public string? Constraints { get; set; }
    
    /// <summary>
    /// Если это какое-то перечесление, то откуда брать значения(константа, enum)
    /// </summary>
    public string? ValueSource { get; set; }
}