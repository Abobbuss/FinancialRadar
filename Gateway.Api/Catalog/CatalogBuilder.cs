using Gateway.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Rules.Abstractions.Catalog;

namespace Gateway.Api.Catalog;

public static class CatalogBuilder
{
    /// <summary>
    /// Собирает снепшот для отправки его в движок
    /// </summary>
    /// <param name="db">Бд</param>
    /// <param name="version">Версия снепшота</param>
    public static async Task<CatalogSnapshot> BuildAsync(AppDbContext db, long version)
    {
        // 1) Домены и значения
        var domainsRaw = await db.ValueDomains
            .AsNoTracking()
            .Include(d => d.Values).ThenInclude(valueDomainValue => valueDomainValue.DataType)
            .ToListAsync();

        var domains = domainsRaw
            .Select(d => new ValueDomainInfo( 
                d.Id, 
                d.Code, 
                d.Values.Select(v => v.Code).ToList(), 
                MapType(d.Values.First().DataType.Code)))
            .ToList();

        // 2) Операторы
        var opsRaw = await db.RuleOperators.AsNoTracking().ToListAsync();
        var ops = opsRaw.Select(o => new RuleOperatorInfo(o.Code)).ToList();

        // 3) Поля + допустимые операторы
        var fieldsRaw = await db.RuleFields
            .AsNoTracking()
            .Include(f => f.DataType)
            .Include(f => f.Operators)
                .ThenInclude(fo => fo.Operator)
            .ToListAsync();

        var fields = fieldsRaw.Select(f 
            => new RuleFieldInfo( 
                Name: f.Name, 
                DataTypeCode: MapType(f.DataType.Code), 
                ValueDomainId: f.ValueDomainId,
                Operators: f.Operators .Select(fo => fo.Operator.Code) .ToList()
        )).ToList();

        // 4) Константы
        var constsRaw = await db.PolicyConstants
            .AsNoTracking()
            .Include(c => c.Type)
            .ToListAsync();

        var consts = constsRaw.Select(c =>
            new PolicyConstantInfo(
                c.Code,
                MapType(c.Type.Code),
                c.Value
            )).ToList();

        return new CatalogSnapshot(
            Fields: fields, Operators: ops, Domains: domains, Constants: consts,
            Version: version, LoadedAtUtc: DateTime.UtcNow
        );
    }

    /// <summary>
    /// Мапиинг для перевода типа данных
    /// </summary>
    private static DataTypeInfo MapType(string code)
        => code.ToLowerInvariant() switch
        {
            "number"  => DataTypeInfo.Number,
            "decimal" => DataTypeInfo.Decimal,
            "string" => DataTypeInfo.String,
            "datetime" => DataTypeInfo.DateTime,
            "enum" => DataTypeInfo.Enum,
            _ => DataTypeInfo.String
        };
}
