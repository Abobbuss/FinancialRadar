using Gateway.Api.DTOs;
using Gateway.Api.Persistence;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Api.Controllers;

/// <summary>
/// Каталог справочников конструктора правил (поля, операторы, типы и т.д.).
/// </summary>
[ApiController]
[Route("api/catalog")]
public class CatalogController(AppDbContext db) : ControllerBase
{
    /// <summary>
    /// Получить список всех полей, по которым можно строить правила.
    /// </summary>
    /// <param name="includeOperators">Включать ли список операторов (по умолчанию true).</param>
    /// <returns>Коллекция полей с типами данных, доменами и допустимыми операторами.</returns>
    [HttpGet("fields")]
    public async Task<ActionResult<IReadOnlyList<RuleFieldDto>>> GetRuleFields([FromQuery] bool includeOperators = true)
    {
        // Базовый запрос с подгрузкой типов данных и доменов
        var query = db.RuleFields
            .AsNoTracking()
            .Include(rf => rf.DataType)
            .Include(rf => rf.ValueDomain)
            .AsQueryable();

        // Если нужно — подгружаем операторов
        if (includeOperators)
        {
            query = query
                .Include(rf => rf.Operators)
                    .ThenInclude(rfo => rfo.Operator);
        }

        var fields = await query
            .OrderBy(rf => rf.Id)
            .Select(rf => new RuleFieldDto
            {
                Id = rf.Id,
                Name = rf.Name,
                DisplayName = rf.DisplayName,
                DataType = new DataTypeDto
                {
                    Id = rf.DataType.Id,
                    Code = rf.DataType.Code
                },
                ValueDomain = rf.ValueDomain == null
                    ? null
                    : new ValueDomainDto
                    {
                        Id = rf.ValueDomain.Id,
                        Code = rf.ValueDomain.Code,
                        DisplayName = rf.ValueDomain.DisplayName
                    },
                Operators = includeOperators
                    ? rf.Operators
                        .OrderBy(o => o.Operator.Id)
                        .Select(o => new OperatorDto
                        {
                            Id = o.Operator.Id,
                            Code = o.Operator.Code,
                            DisplayName = o.Operator.DisplayName
                        })
                        .ToList()
                    : new List<OperatorDto>()
            })
            .ToListAsync();

        return Ok(fields);
    }
}
