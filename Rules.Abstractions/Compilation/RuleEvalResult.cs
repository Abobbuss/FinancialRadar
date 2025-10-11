namespace Rules.Abstractions.Compilation;

/// <summary>
/// Результат применения одного правила к транзакции.
/// </summary>
public sealed record RuleEvalResult(
    string Status,          // "matched" | "not_matched" | "error"
    float ScoreApplied,     // вес, применённый этим правилом (часто 0 — вес прибавит вызывающая сторона)
    object? Details         // любые сериализуемые детали для аудита
)
{
    /// <summary>
    /// Правила было успешно применено.
    /// </summary>
    public static RuleEvalResult Matched(object? details = null) =>
        new("matched", 0, details);

    /// <summary>
    /// Правило было нарушено.
    /// </summary>
    public static RuleEvalResult NotMatched(float score, object? details = null) =>
        new("not_matched", score, details);
}