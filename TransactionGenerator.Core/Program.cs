using TransactionGenerator.Models;
using TransactionGenerator.Services;

namespace TransactionGenerator;

class Program
{
    private static readonly ITransactionGenerator _generator;
    private static readonly List<GenerationResult> _generationResults = new();
    private static readonly string _outputDirectory;
    
    static Program()
    {
        // Явно указываем папку для сохранения - рядом с .exe или в папке проекта
        _outputDirectory = GetOutputDirectory();
        
        var csvService = new CsvService(_outputDirectory);
        _generator = new Services.TransactionGenerator(csvService);
        
        Console.WriteLine($"📁 Файлы будут сохраняться в: {_outputDirectory}");
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        DisplayWelcomeMessage();
        
        while (true)
        {
            DisplayMainMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GenerateNormalTransactions();
                    break;
                case "2":
                    GenerateSuspiciousTransactions();
                    break;
                case "3":
                    GenerateMixedTransactions();
                    break;
                case "4":
                    DisplayGenerationHistory();
                    break;
                case "5":
                    OpenOutputDirectory();
                    break;
                case "0":
                    Console.WriteLine("👋 До свидания!");
                    return;
                default:
                    Console.WriteLine("❌ Неверный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    private static string GetOutputDirectory()
    {
        // Пытаемся найти папку проекта
        var currentDir = Directory.GetCurrentDirectory();
        var projectDir = currentDir;
        
        // Если мы в bin/Debug/net8.0, поднимаемся на 3 уровня выше
        if (currentDir.Contains("bin") && currentDir.Contains("Debug"))
        {
            var directoryInfo = Directory.GetParent(currentDir);
            for (int i = 0; i < 3; i++)
            {
                directoryInfo = directoryInfo?.Parent;
                if (directoryInfo != null)
                {
                    projectDir = directoryInfo.FullName;
                }
            }
        }
        
        var outputDir = Path.Combine(projectDir, "GeneratedTransactions");
        
        // Создаем папку, если не существует
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        
        return outputDir;
    }

    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("🚀 ГЕНЕРАТОР ТЕСТОВЫХ ТРАНЗАКЦИЙ");
        Console.WriteLine("         (CSV Export Version)");
        Console.WriteLine("==========================================");
        Console.WriteLine("Автоматическая генерация и сохранение");
        Console.WriteLine("транзакций в CSV файлы");
        Console.WriteLine("==========================================");
    }

    static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("\n📋 ГЛАВНОЕ МЕНЮ:");
        Console.WriteLine("1. 📊 Сгенерировать ОБЫЧНЫЕ транзакции (CSV)");
        Console.WriteLine("2. 🚨 Сгенерировать ПОДОЗРИТЕЛЬНЫЕ транзакции (CSV)");
        Console.WriteLine("3. 🔀 Сгенерировать СМЕШАННЫЕ транзакции (CSV)");
        Console.WriteLine("4. 📈 Показать историю генерации");
        Console.WriteLine("5. 📁 Открыть папку с файлами");
        Console.WriteLine("0. ❌ Выход");
        Console.Write("\nВыберите опцию: ");
    }

     static void GenerateNormalTransactions()
    {
        Console.Write("\nВведите количество обычных транзакций: ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
        {
            var fileName = $"normal_transactions_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            var fullPath = Path.Combine(_outputDirectory, fileName);
            
            var result = _generator.GenerateNormalTransactions(count, fullPath);
            _generationResults.Add(result);
            
            DisplayGenerationResult(result, "ОБЫЧНЫЕ");
        }
        else
        {
            Console.WriteLine("❌ Неверное количество транзакций.");
        }
    }

    static void GenerateSuspiciousTransactions()
    {
        Console.Write("\nВведите количество подозрительных транзакций: ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
        {
            var fileName = $"suspicious_transactions_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            var fullPath = Path.Combine(_outputDirectory, fileName);
            
            var result = _generator.GenerateSuspiciousTransactions(count, fullPath);
            _generationResults.Add(result);
            
            DisplayGenerationResult(result, "ПОДОЗРИТЕЛЬНЫЕ");
        }
        else
        {
            Console.WriteLine("❌ Неверное количество транзакций.");
        }
    }

    static void GenerateMixedTransactions()
    {
        Console.Write("\nВведите общее количество транзакций: ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
        {
            Console.Write("Доля подозрительных транзакций (0.1 = 10%): ");
            if (double.TryParse(Console.ReadLine(), out double ratio) && ratio >= 0 && ratio <= 1)
            {
                var fileName = $"mixed_transactions_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                var fullPath = Path.Combine(_outputDirectory, fileName);
                
                var result = _generator.GenerateMixedTransactions(count, ratio, fullPath);
                _generationResults.Add(result);
                
                DisplayGenerationResult(result, "СМЕШАННЫЕ");
            }
            else
            {
                Console.WriteLine("❌ Неверная доля подозрительных транзакций.");
            }
        }
        else
        {
            Console.WriteLine("❌ Неверное количество транзакций.");
        }
    }
    static void DisplayGenerationResult(GenerationResult result, string type)
    {
        Console.WriteLine($"\n✅ УСПЕШНО СГЕНЕРИРОВАНО!");
        Console.WriteLine("==========================================");
        Console.WriteLine($"Тип: {type}");
        Console.WriteLine($"📊 Общее количество: {result.TotalCount}");
        Console.WriteLine($"✅ Обычных: {result.NormalCount}");
        Console.WriteLine($"🚨 Подозрительных: {result.SuspiciousCount}");
        Console.WriteLine($"📅 Время генерации: {result.GeneratedAt:dd.MM.yyyy HH:mm:ss}");
        Console.WriteLine($"💾 Файл: {result.FilePath}");
        Console.WriteLine("==========================================");
        
        // Показываем превью первых 3 транзакций
        if (result.Transactions.Any())
        {
            Console.WriteLine("\n👀 ПРЕВЬЮ ДАННЫХ (первые 3 транзакции):");
            foreach (var transaction in result.Transactions.Take(3))
            {
                DisplayTransactionPreview(transaction);
            }
        }
    }

    static void DisplayTransactionPreview(Transaction transaction)
    {
        var status = transaction.IsSuspicious ? "🚨" : "✅";
        Console.WriteLine($"{status} {transaction.BankFrom} → {transaction.BankTo}");
        Console.WriteLine($"   💰 {transaction.MoneyCount} {transaction.MoneyFormat}");
        Console.WriteLine($"   📍 {transaction.Location} | 🏷️ {transaction.MerchantCategory}");
        Console.WriteLine("   " + new string('─', 40));
    }

    static void DisplayGenerationHistory()
    {
        if (!_generationResults.Any())
        {
            Console.WriteLine("\n📭 История генерации пуста.");
            return;
        }

        Console.WriteLine($"\n📊 ИСТОРИЯ ГЕНЕРАЦИИ ({_generationResults.Count} операций):");
        Console.WriteLine("==========================================");

        int totalTransactions = 0;
        int totalSuspicious = 0;

        for (int i = 0; i < _generationResults.Count; i++)
        {
            var result = _generationResults[i];
            
            Console.WriteLine($"Операция #{i + 1}:");
            Console.WriteLine($"   📅 {result.GeneratedAt:dd.MM.yyyy HH:mm}");
            Console.WriteLine($"   🔢 Транзакций: {result.TotalCount}");
            Console.WriteLine($"   🚨 Подозрительных: {result.SuspiciousCount}");
            Console.WriteLine($"   💾 Файл: {Path.GetFileName(result.FilePath)}");
            Console.WriteLine("   ---");
            
            totalTransactions += result.TotalCount;
            totalSuspicious += result.SuspiciousCount;
        }

        Console.WriteLine($"\n📈 ВСЕГО СГЕНЕРИРОВАНО:");
        Console.WriteLine($"   Транзакций: {totalTransactions}");
        Console.WriteLine($"   Подозрительных: {totalSuspicious}");
        Console.WriteLine($"   Доля подозрительных: {(double)totalSuspicious / totalTransactions:P2}");
    }

    static void OpenOutputDirectory()
    {
        try
        {
            Console.WriteLine($"\n📁 Папка сохранения: {_outputDirectory}");
            
            if (Directory.Exists(_outputDirectory))
            {
                var csvFiles = Directory.GetFiles(_outputDirectory, "*.csv");
                if (csvFiles.Any())
                {
                    Console.WriteLine($"\n📄 Найдено CSV файлов: {csvFiles.Length}");
                    foreach (var file in csvFiles)
                    {
                        var fileInfo = new FileInfo(file);
                        Console.WriteLine($"   📋 {fileInfo.Name} ({fileInfo.Length} bytes)");
                    }
                }
                else
                {
                    Console.WriteLine("❌ CSV файлы не найдены в папке сохранения.");
                }
            }
            else
            {
                Console.WriteLine("❌ Папка сохранения не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка при открытии директории: {ex.Message}");
        }
    }
}