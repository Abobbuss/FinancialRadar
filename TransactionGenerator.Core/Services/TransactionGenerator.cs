using Bogus;
using TransactionGenerator.Enums;
using TransactionGenerator.Models;

namespace TransactionGenerator.Services;

public class TransactionGenerator : ITransactionGenerator
{
    private readonly Faker<Transaction> _normalTransactionFaker;
    private readonly Faker<Transaction> _suspiciousTransactionFaker;
    private readonly ICsvService _csvService;

    public TransactionGenerator(ICsvService csvService)
    {
        _csvService = csvService;

        // Генератор обычных транзакций
        _normalTransactionFaker = new Faker<Transaction>("ru")
            .RuleFor(t => t.GlobalId, f => f.Random.Guid())
            .RuleFor(t => t.BankFrom, f => f.PickRandom(BankNames.RussianBanks))
            .RuleFor(t => t.BankTo, f => f.PickRandom(BankNames.RussianBanks))
            .RuleFor(t => t.MoneyCount, f => f.Finance.Amount(100, 50000, 2))
            .RuleFor(t => t.MoneyFormat, f => f.PickRandom("RUB", "USD", "EUR"))
            .RuleFor(t => t.TransactionDate, f => f.Date.Recent(30))
            .RuleFor(t => t.CreatedAt, f => DateTime.UtcNow)
            .RuleFor(t => t.MerchantCategory, f => f.PickRandom(MerchantCategories.NormalCategories))
            .RuleFor(t => t.Location, f => f.PickRandom(Locations.RussianCities))
            .RuleFor(t => t.DeviceId, f => f.Random.Replace("DEV-#####"))
            .RuleFor(t => t.IsSuspicious, f => false)
            .RuleFor(t => t.TransactionType, f => f.PickRandom("Purchase", "Transfer", "Payment"))
            .RuleFor(t => t.RiskLevel, f => "LOW");

        // Генератор подозрительных транзакций
        _suspiciousTransactionFaker = new Faker<Transaction>("ru")
            .RuleFor(t => t.GlobalId, f => f.Random.Guid())
            .RuleFor(t => t.BankFrom, f => f.PickRandom(BankNames.SuspiciousBanks))
            .RuleFor(t => t.BankTo, f => f.PickRandom(BankNames.RussianBanks))
            .RuleFor(t => t.MoneyCount, f => f.Finance.Amount(100000, 5000000, 2))
            .RuleFor(t => t.MoneyFormat, f => f.PickRandom("USD", "EUR", "CNY"))
            .RuleFor(t => t.TransactionDate, f => f.Date.Recent(1))
            .RuleFor(t => t.CreatedAt, f => DateTime.UtcNow)
            .RuleFor(t => t.MerchantCategory, f => f.PickRandom(MerchantCategories.SuspiciousCategories))
            .RuleFor(t => t.Location, f => f.PickRandom(Locations.SuspiciousLocations))
            .RuleFor(t => t.DeviceId, f => f.Random.Replace("ANON-#####"))
            .RuleFor(t => t.IsSuspicious, f => true)
            .RuleFor(t => t.TransactionType, f => f.PickRandom("Large Transfer", "Cryptocurrency", "Investment"))
            .RuleFor(t => t.RiskLevel, f => "HIGH");
    }

    public GenerationResult GenerateNormalTransactions(int count, string? filePath = null)
    {
        var transactions = _normalTransactionFaker.Generate(count);
        filePath ??= $"normal_transactions_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        return SaveResult(transactions, "Normal", filePath);
    }

    public GenerationResult GenerateSuspiciousTransactions(int count, string? filePath = null)
    {
        var transactions = _suspiciousTransactionFaker.Generate(count);
        filePath ??= $"suspicious_transactions_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        return SaveResult(transactions, "Suspicious", filePath);
    }

    public GenerationResult GenerateMixedTransactions(int count, double suspiciousRatio = 0.1, string? filePath = null)
    {
        var transactions = new List<Transaction>();
        int suspiciousCount = (int)(count * suspiciousRatio);
        int normalCount = count - suspiciousCount;

        transactions.AddRange(_normalTransactionFaker.Generate(normalCount));
        transactions.AddRange(_suspiciousTransactionFaker.Generate(suspiciousCount));

        transactions = transactions.OrderBy(x => Guid.NewGuid()).ToList();
        
        filePath ??= $"mixed_transactions_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        return SaveResult(transactions, "Mixed", filePath);
    }
    
    public Transaction GenerateSingleTransaction(bool suspicious = false)
    {
        return suspicious ? 
            _suspiciousTransactionFaker.Generate() : 
            _normalTransactionFaker.Generate();
    }

    private GenerationResult SaveResult(List<Transaction> transactions, string batchType, string? filePath = null)
    {
        var result = new GenerationResult 
        { 
            Transactions = transactions 
        };

        // Сохраняем в CSV
        if (!string.IsNullOrEmpty(filePath))
        {
            result.FilePath = _csvService.SaveToCsv(transactions, filePath);
        }
        else
        {
            var fileName = $"{batchType.ToLower()}_transactions_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            result.FilePath = _csvService.SaveToCsv(transactions, fileName);
        }

        return result;
    }
}