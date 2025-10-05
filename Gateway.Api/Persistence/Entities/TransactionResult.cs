using System.ComponentModel.DataAnnotations;

namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Результат апрува транзакции
/// </summary>
public class TransactionResult
{
    /// <summary>
    /// Pk
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Fk на транзакцию
    /// </summary>
    public long TransactionId { get; set; }
    
    /// <summary>
    /// Fk на транзакцию
    /// </summary>
    public Transaction Transaction { get; set; } = default!;

    /// <summary>
    /// FK -> ValueDomainValue (результат транзакции)
    /// </summary>
    public int CurrencyId { get; set; }
    
    /// <summary>
    /// FK -> ValueDomainValue (результат транзакции)
    /// </summary>
    public ValueDomainValue Currency { get; set; } = default!;
    
    /// <summary>
    /// Итоговый риск по транзакции
    /// </summary>
    public decimal RiskScore { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Список примененных правил к транзакции
    /// </summary>
    public List<RuleEvaluation> Evaluations { get; set; } = new();
}