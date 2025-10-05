namespace Gateway.Api.DTOs;

/// <summary>
/// DTO для описания поля, по которому можно создавать правила.
/// </summary>
public record RuleFieldDto
{
    /// <summary>Уникальный идентификатор поля.</summary>
    public int Id { get; init; }

    /// <summary>Машинное имя поля (например "amount", "currency", "ts").</summary>
    public string Name { get; init; } = default!;

    /// <summary>Отображаемое имя для UI.</summary>
    public string DisplayName { get; init; } = default!;

    /// <summary>Тип данных, с которым работает это поле.</summary>
    public DataTypeDto DataType { get; init; } = default!;

    /// <summary>Справочник возможных значений (если поле перечислимое), иначе null.</summary>
    public ValueDomainDto? ValueDomain { get; init; }

    /// <summary>Список доступных операторов для данного поля.</summary>
    public IReadOnlyList<OperatorDto> Operators { get; init; } = new List<OperatorDto>();
}