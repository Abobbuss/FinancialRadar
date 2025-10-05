using System.ComponentModel.DataAnnotations;

namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Список примененных правил к транзакции
/// </summary>
public class RuleEvaluation
{
    /// <summary>
    /// Pk
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Fk на транзакцию
    /// </summary>
    public long TransactionResultId { get; set; }
    
    /// <summary>
    /// Fk на транзакцию
    /// </summary>
    public TransactionResult TransactionResult { get; set; } = default!;

    /// <summary>
    /// Fk на правило примененное
    /// </summary>
    public long RuleVersionId { get; set; }
    
    /// <summary>
    /// Fk на транзакцию
    /// </summary>
    public RuleVersion RuleVersion { get; set; } = default!;

    public bool IsPassed { get; set; } = true;
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}