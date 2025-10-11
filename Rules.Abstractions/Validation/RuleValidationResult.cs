using System.Text.Json;

namespace Rules.Abstractions.Validation;

/// <summary>
/// Результат валидации DSL.
/// </summary>
public sealed record RuleValidationResult(
    bool IsValid,
    JsonDocument? Normalized,
    IReadOnlyList<string> Errors
)
{
    /// <summary>
    /// Валидация правила прошла успешно
    /// </summary>
    public static RuleValidationResult Ok(JsonDocument normalized) =>
        new(true, normalized, Array.Empty<string>());

    /// <summary>
    /// Валидация правила проволилась, см список ошибок
    /// </summary>
    public static RuleValidationResult Fail(IReadOnlyList<string> errors) =>
        new(false, null, errors);
}