namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Справочник для enum значений
/// </summary>
public class ValueDomain
{
    /// <summary>
    /// Pk
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Машинное название ( transfer_types, weekDays)
    /// </summary>
    public string Code { get; set; } = default!;
    
    /// <summary>
    /// Отображаемое имя для enum значений в UI
    /// </summary>
    public string DisplayName { get; set; } = default!;
    
    /// <summary>
    /// Данные этого энама
    /// </summary>
    public List<ValueDomainValue> Values { get; set; } = new();
}