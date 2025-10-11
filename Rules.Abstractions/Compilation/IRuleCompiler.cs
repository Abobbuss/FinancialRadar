using System.Text.Json;

namespace Rules.Abstractions.Compilation;

/// <summary>
/// Компилятор валидного (нормализованного) DSL в исполняемое правило.
/// </summary>
public interface IRuleCompiler
{
    /// <summary>
    /// Компилирует нормализованный JSON DSL в исполняемый объект.
    /// Может бросить исключение при нарушении инвариантов normalized DSL.
    /// </summary>
    IExecutableRule Compile(JsonDocument normalizedDsl);
}