namespace Rules.Abstractions.Compilation;

/// <summary>
/// Исполняемое правило. Предполагается потокобезопасным и неизменяемым.
/// </summary>
public interface IExecutableRule
{
    /// <summary>
    /// Оценивает транзакцию относительно текущего правила.
    /// Не должен бросать исключения при валидном входе; ошибки в Status="error".
    /// </summary>
    RuleEvalResult Evaluate(TransactionContext tx);
}