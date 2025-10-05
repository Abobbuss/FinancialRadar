using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Весь список правил когда либо созданных
/// </summary>
public class RuleVersion
{
    /// <summary>
    /// Pk
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Глобал Id правила
    /// </summary>
    public Guid RuleKey { get; set; }
    
    /// <summary>
    /// Версия правила
    /// </summary>
    public int Version { get; set; }
    
    /// <summary>
    /// Описание правила, то что делает это правило
    /// </summary>
    public JsonDocument Description { get; set; } = JsonDocument.Parse("{}");

    /// <summary>
    /// Имя правила для UI
    /// </summary>
    public string DisplayName { get; set; } = default!;
    
    /// <summary>
    /// Описание правила для UI
    /// </summary>
    public string? DisplayDescription { get; set; }
    
    /// <summary>
    /// Риск при невыполнении правила (от 0 до 1)
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Было ли удалено это правило
    /// </summary>
    public bool IsDeleted { get; set; } = false;
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}