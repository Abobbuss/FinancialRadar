using System.Text.Json;

namespace Rules.Abstractions.Validation;

/// <summary>
/// Валидатор DSL правил: проверяет структуру и совместимость с каталогом,
/// возвращает каноническую (нормализованную) форму JSON или список ошибок.
/// </summary>
public interface IRuleValidator
{
    /// <summary>
    /// Проверяет DSL и, при необходимости, нормализует (канонизирует) его.
    /// Исключения не бросает на пользовательских ошибках.
    /// </summary>
    Task<RuleValidationResult> TryValidateAndNormalizeAsync(
        JsonElement dsl,
        CancellationToken ct = default);
}