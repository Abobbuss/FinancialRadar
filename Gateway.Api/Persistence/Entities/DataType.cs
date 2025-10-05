namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Справочник типы данных 
/// </summary>
public class DataType
{
    /// <summary>
    /// Pk
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Представляет описание типа данных int, dateTime и тд.
    /// </summary>
    public string Code { get; set; } = default!;
    
    /// <summary>
    /// Описание типа данных (если будут какие-то нестандартные)
    /// </summary>
    public string? Description { get; set; }
}