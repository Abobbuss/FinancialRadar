namespace Gateway.Api.DTOs;

/// <summary>
/// DTO для описания операторов сравнения, доступных при создании правил.
/// </summary>
public record OperatorDto
{
    /// <summary>Уникальный идентификатор оператора.</summary>
    public int Id { get; init; }

    /// <summary>Машинный код оператора (==, !=, &gt;, &lt;, between и т.д.).</summary>
    public string Code { get; init; } = default!;

    /// <summary>Отображаемое имя оператора для UI.</summary>
    public string DisplayName { get; init; } = default!;
}