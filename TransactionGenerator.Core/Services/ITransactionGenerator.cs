using TransactionGenerator.Models;

namespace TransactionGenerator.Services;

public interface ITransactionGenerator
{
    GenerationResult GenerateNormalTransactions(int count, string? filePath = null);
    GenerationResult GenerateSuspiciousTransactions(int count, string? filePath = null);
    GenerationResult GenerateMixedTransactions(int count, double suspiciousRatio = 0.1, string? filePath = null);
    Transaction GenerateSingleTransaction(bool suspicious = false);
}