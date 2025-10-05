using System.Globalization;
using System.Text;
using TransactionGenerator.Models;

namespace TransactionGenerator.Services;

public class CsvService : ICsvService
{
    private readonly string _outputDirectory;

    public CsvService(string outputDirectory)
    {
        _outputDirectory = outputDirectory;
        
        // Создаем папку, если не существует
        if (!Directory.Exists(_outputDirectory))
        {
            Directory.CreateDirectory(_outputDirectory);
        }
    }

    public string SaveToCsv(List<Transaction> transactions, string filePath)
    {
        try
        {
            // Если передан только filename, добавляем путь к output directory
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.Combine(_outputDirectory, filePath);
            }

            // Создаем директорию, если не существует
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            
            // Записываем заголовок
            writer.WriteLine(GetCsvHeader());
            
            // Записываем данные
            foreach (var transaction in transactions)
            {
                writer.WriteLine(ConvertToCsvLine(transaction));
            }

            Console.WriteLine($"✅ Файл успешно сохранен: {filePath}");
            return Path.GetFullPath(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка при сохранении файла: {ex.Message}");
            throw;
        }
    }
    public string GetCsvHeader()
    {
        return "GlobalId,BankFrom,BankTo,MoneyCount,MoneyFormat,TransactionDate,CreatedAt,MerchantCategory,Location,DeviceId,IsSuspicious,TransactionType,RiskLevel";
    }

    public string ConvertToCsvLine(Transaction transaction)
    {
        var fields = new List<string>
        {
            transaction.GlobalId.ToString(),
            EscapeCsvField(transaction.BankFrom),
            EscapeCsvField(transaction.BankTo),
            transaction.MoneyCount.ToString(CultureInfo.InvariantCulture),
            EscapeCsvField(transaction.MoneyFormat),
            transaction.TransactionDate.ToString("O"),
            transaction.CreatedAt.ToString("O"),
            EscapeCsvField(transaction.MerchantCategory),
            EscapeCsvField(transaction.Location),
            EscapeCsvField(transaction.DeviceId),
            transaction.IsSuspicious.ToString(),
            EscapeCsvField(transaction.TransactionType),
            EscapeCsvField(transaction.RiskLevel)
        };

        return string.Join(",", fields);
    }

    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field))
            return string.Empty;

        // Если поле содержит запятые, кавычки или переносы строк - заключаем в кавычки
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }

        return field;
    }
}