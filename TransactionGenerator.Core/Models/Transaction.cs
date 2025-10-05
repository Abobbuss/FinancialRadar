namespace TransactionGenerator.Models;

public class Transaction
{
    public Guid GlobalId { get; set; }
    public string BankFrom { get; set; } = string.Empty;
    public string BankTo { get; set; } = string.Empty;
    public decimal MoneyCount { get; set; }
    public string MoneyFormat { get; set; } = "RUB";
    public DateTime TransactionDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string MerchantCategory { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string DeviceId { get; set; } = string.Empty;
    public bool IsSuspicious { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = string.Empty;
}

public class GenerationResult
{
    public List<Transaction> Transactions { get; set; } = new();
    public int TotalCount => Transactions.Count;
    public int NormalCount => Transactions.Count(t => !t.IsSuspicious);
    public int SuspiciousCount => Transactions.Count(t => t.IsSuspicious);
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public string FilePath { get; set; } = string.Empty;
}