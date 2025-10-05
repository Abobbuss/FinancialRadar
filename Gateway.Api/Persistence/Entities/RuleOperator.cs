using System.ComponentModel.DataAnnotations;

namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Операторы и их применимость
/// </summary>
public class RuleOperator
{
    /// <summary>
    /// Pk
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Машинный код !=, == и тд.
    /// </summary>
    public string Code { get; set; } = default!;
    
    /// <summary>
    /// Отображаемое имя для UI
    /// </summary>
    public string DisplayName { get; set; } = default!;

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<RuleFieldOperator> Fields { get; set; } = new();
}