namespace TransactionGenerator.Enums;

public static class BankNames
{
    public static readonly string[] RussianBanks = 
    {
        "Сбербанк", "Альфа-Банк", "Тинькофф", "ВТБ", 
        "Газпромбанк", "Райффайзенбанк", "Открытие", "Промсвязьбанк",
        "Россельхозбанк", "Совкомбанк", "МКБ", "ЮниКредит Банк"
    };

    public static readonly string[] SuspiciousBanks = 
    {
        "OffshoreBank Ltd", "QuickMoney Corp", "FastCash International",
        "CryptoTransfer AG", "AnonymousPay LLC", "DigitalAssets Bank"
    };
}

public static class MerchantCategories
{
    public static readonly string[] NormalCategories = 
    {
        "Retail", "Electronics", "Food & Dining", "Travel",
        "Entertainment", "Healthcare", "Utilities", "Online Services"
    };

    public static readonly string[] SuspiciousCategories = 
    {
        "Cryptocurrency", "Forex Trading", "High-Risk Investments",
        "Offshore Services", "Anonymous Transactions"
    };
}

public static class Locations
{
    public static readonly string[] RussianCities = 
    {
        "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург",
        "Казань", "Нижний Новгород", "Краснодар", "Владивосток"
    };

    public static readonly string[] SuspiciousLocations = 
    {
        "Offshore", "International", "Digital", "Anonymous"
    };
}