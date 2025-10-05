using System.ComponentModel.DataAnnotations;

namespace Gateway.Api.Persistence.Entities;

/// <summary>
/// Транзакция
/// </summary>
public class Transaction
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Id транзакции глобальный
    /// </summary>
    public Guid GlobalId { get; set; }
    
    /// <summary>
    /// Название банка откуда пришла транзакция
    /// </summary>
    public string BankFrom { get; set; } = default!;
    
    /// <summary>
    /// Транзакция банка куда идет транзакция
    /// </summary>
    public string BankTo { get; set; } = default!;
    
    /// <summary>
    /// Количество денег в переводе
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// FK -> ValueDomainValue (currencies)
    /// </summary>
    public int CurrencyId { get; set; }
    
    /// <summary>
    /// FK -> ValueDomainValue (currencies)
    /// </summary>
    public ValueDomainValue Currency { get; set; } = default!;
    
    /// <summary>
    /// Время появления транзакции
    /// </summary>
    public DateTime TransactionDate { get; set; }
}