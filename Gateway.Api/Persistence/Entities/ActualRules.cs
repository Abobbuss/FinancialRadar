using System.ComponentModel.DataAnnotations;

namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Правила которые сейчас используются внутри UI
/// </summary>
public class ActualRules
{
    /// <summary>
    /// Pk
    /// </summary>
    [Key]
    public Guid RuleKey { get; set; }
    
    /// <summary>
    /// FK на версию правила в RuleVersion
    /// </summary>
    public long RuleVersionId { get; set; }
    
    /// <summary>
    /// FK на версию правила в RuleVersion
    /// </summary>
    public RuleVersion RuleVersion { get; set; } = default!;
    
    /// <summary>
    /// Используется ли это правило для отбора транзакций
    /// </summary>
    public bool IsActive { get; set; } = false;
}