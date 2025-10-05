namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Значения enum
/// </summary>
public class ValueDomainValue
{
    /// <summary>
    /// Pk
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// FK на таблицу с этим enum
    /// </summary>
    public int ValueDomainId { get; set; }
    
    /// <summary>
    /// FK на таблицу с этим enum
    /// </summary>
    public ValueDomain ValueDomain { get; set; } = default!;
    
    /// <summary>
    /// FK на таблицу с типом данных
    /// </summary>
    public int DataTypeId { get; set; }
    
    /// <summary>
    /// FK на таблицу с типом данных
    /// </summary>
    public DataType DataType { get; set; }  = default!;
    
    /// <summary>
    /// Машинное название (RUB, USD и так далее)
    /// </summary>
    public string Code { get; set; } = default!;
    
    /// <summary>
    /// Отображаемое имя для UI
    /// </summary>
    public string Label { get; set; } = default!;
}