namespace Rules.Abstractions.Catalog;

/// <summary>
/// Справочник типов данных
/// </summary>
public enum DataTypeInfo
{
    /// <summary>
    /// Целое
    /// </summary>
    Number,
    
    /// <summary>
    /// Строковое
    /// </summary>
    String,
    
    /// <summary>
    /// Дата и время
    /// </summary>
    DateTime,
    
    /// <summary>
    /// Энам
    /// </summary>
    Enum,
    
    /// <summary>
    /// Децимал
    /// </summary>
    Decimal
}