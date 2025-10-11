namespace Rules.Abstractions.Compilation;

/// <summary>
/// Контекст транзакции, передаваемый в исполняемое правило.
/// </summary>
public sealed record TransactionContext(
    Guid GlobalId,
    decimal Amount,
    string CurrencyCode,
    DateTime TimestampUtc,
    string FromBank,
    string ToBank,
    IReadOnlyDictionary<string, object>? Extras // для дополнительных полей
);