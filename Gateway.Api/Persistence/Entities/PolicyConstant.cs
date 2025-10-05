using System.ComponentModel.DataAnnotations;

namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Всякие константы
/// </summary>
public class PolicyConstant
{
    /// <summary>
    /// Pk
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Fk -> DataType
    /// </summary>
    public int TypeId { get; set; }
    
    /// <summary>
    /// Fk -> DataType
    /// </summary>
    public DataType Type { get; set; } = default!;
    
    /// <summary>
    /// Машинное описание LIMIT_NO_PIN, MICRO_N...
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// Описание для UI
    /// </summary>
    public string? Description { get; set; }
}