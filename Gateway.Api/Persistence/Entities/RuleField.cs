using System.ComponentModel.DataAnnotations;

namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Поля по которым можно создавать правила
/// </summary>
public class RuleField
{
    /// <summary>
    /// Pk
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Машинное имя amount, dateTrans и тд.
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Отоюражаемое имя в UI
    /// </summary>
    public string DisplayName { get; set; } = default!;
    
    /// <summary>
    /// Fk на DataType  Тип данных с которыми работает это поле
    /// </summary>
    public int DataTypeId { get; set; }
    
    /// <summary>
    /// Fk на DataType  Тип данных с которыми работает это поле
    /// </summary>
    public DataType DataType { get; set; } = default!;
    
    /// <summary>
    /// Fk на ValueDomain если это enum
    /// </summary>
    public int? ValueDomainId { get; set; }
    
    /// <summary>
    /// Fk на ValueDomain если это enum
    /// </summary>
    public ValueDomain? ValueDomain { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Список операторов, с которым может работать это поле
    /// </summary>
    public List<RuleFieldOperator> Operators { get; set; } = new();
}