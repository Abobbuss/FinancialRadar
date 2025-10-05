namespace Gateway.Api.DTOs;

/// <summary>
/// DTO для справочников перечислимых значений (enum), например: валюты, типы переводов.
/// </summary>
public record ValueDomainDto
{
    /// <summary>Идентификатор домена (enum-группы).</summary>
    public int Id { get; init; }

    /// <summary>Машинное имя домена (например "currencies", "transfer_types").</summary>
    public string Code { get; init; } = default!;

    /// <summary>Отображаемое имя домена для UI.</summary>
    public string DisplayName { get; init; } = default!;
}