namespace Gateway.Api.DTOs;

/// <summary>
/// DTO, представляющее тип данных (например number, string, datetime).
/// </summary>
public record DataTypeDto
{
    /// <summary>Уникальный идентификатор типа данных.</summary>
    public int Id { get; init; }

    /// <summary>Машинное имя типа (например "number", "string", "datetime").</summary>
    public string Code { get; init; } = default!;
}