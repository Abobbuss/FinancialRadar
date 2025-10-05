using TransactionGenerator.Models;

namespace TransactionGenerator.Services;

public interface ICsvService
{
    string SaveToCsv(List<Transaction> transactions, string filePath);
    string GetCsvHeader();
    string ConvertToCsvLine(Transaction transaction);
}