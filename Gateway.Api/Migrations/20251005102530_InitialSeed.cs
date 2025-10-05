using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gateway.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder m)
        {
            // data_type
            m.InsertData("data_types", new[] { "id", "code", "description" }, new object[,]
            {
                { 1, "number", "Numeric" },
                { 2, "string", "Text" },
                { 3, "datetime", "DateTime" },
                { 4, "enum", "Enumerated" },
            });

            // value_domain
            m.InsertData("value_domains", new[] { "id", "code", "display_name" }, new object[,]
            {
                { 1, "currencies", "Валюты" }
            });

            // value_domain_value
            m.InsertData("value_domain_values",
                new[] { "id", "value_domain_id", "data_type_id", "code", "label" },
                new object[,]
                {
                    { 101, 1, 4, "RUB", "Российский рубль" },
                    { 102, 1, 4, "USD", "Доллар США" },
                    { 103, 1, 4, "EUR", "Евро" },
                });

            // rule_operator
            m.InsertData("rule_operators", new[] { "id", "code", "display_name", "created_at" }, new object[,]
            {
                { 1, "==", "Равно", DateTime.UtcNow },
                { 2, ">",  "Больше", DateTime.UtcNow },
                { 3, "<",  "Меньше", DateTime.UtcNow },
                { 4, ">=", "Больше или равно", DateTime.UtcNow },
                { 5, "<=", "Меньше или равно", DateTime.UtcNow },
                { 6, "in", "В списке", DateTime.UtcNow },
                { 7, "between", "Между", DateTime.UtcNow },
                { 8, "hour_ge", "Час ≥", DateTime.UtcNow },
                { 9, "hour_between", "Час в диапазоне", DateTime.UtcNow },
            });

            // rule_field
            m.InsertData("rule_fields",
                new[] { "id", "name", "display_name", "data_type_id", "value_domain_id", "created_at" },
                new object[,]
                {
                    { 1, "amount",   "Сумма", 1, null, DateTime.UtcNow },
                    { 2, "currency", "Валюта", 4, 1,   DateTime.UtcNow },
                    { 3, "ts",       "Время", 3, null, DateTime.UtcNow },
                });

            // rule_field_operator (пары)
            m.InsertData("rule_field_operators", new[] { "field_id", "operator_id", "constraints", "value_source" }, new object[,]
            {
                { 1, 2,  "{\"min\":0}",         "literal" },   // amount >
                { 1, 3,  "{\"min\":0}",         "literal" },   // amount <
                { 1, 4,  "{\"min\":0}",         "literal" },   // amount >=
                { 1, 5,  "{\"min\":0}",         "literal" },   // amount <=
                { 1, 7,  "{\"min\":0}",         "literal" },   // amount between
                { 2, 1,  null,                  "domain"  },   // currency ==
                { 2, 6,  null,                  "domain"  },   // currency in
                { 3, 8,  "{\"min\":0,\"max\":23}", "literal" },// ts hour_ge
                { 3, 9,  "{\"min\":0,\"max\":23}", "literal" },// ts hour_between
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder m)
        {
            // 1) Связующая таблица поле↔оператор (составной ключ)
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 1, 2 });
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 1, 3 });
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 1, 4 });
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 1, 5 });
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 1, 7 });
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 2, 1 });
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 2, 6 });
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 3, 8 });
            m.DeleteData(
                table: "rule_field_operators",
                keyColumns: new[] { "field_id", "operator_id" },
                keyValues: new object[] { 3, 9 });

            // 2) Поля конструктора
            m.DeleteData(table: "rule_fields", keyColumn: "id", keyValue: 1);
            m.DeleteData(table: "rule_fields", keyColumn: "id", keyValue: 2);
            m.DeleteData(table: "rule_fields", keyColumn: "id", keyValue: 3);

            // 3) Операторы
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 1);
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 2);
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 3);
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 4);
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 5);
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 6);
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 7);
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 8);
            m.DeleteData(table: "rule_operators", keyColumn: "id", keyValue: 9);

            // 4) Значения домена (валюты)
            m.DeleteData(table: "value_domain_values", keyColumn: "id", keyValue: 101);
            m.DeleteData(table: "value_domain_values", keyColumn: "id", keyValue: 102);
            m.DeleteData(table: "value_domain_values", keyColumn: "id", keyValue: 103);

            // 5) Домены
            m.DeleteData(table: "value_domains", keyColumn: "id", keyValue: 1);

            // 6) Типы данных
            m.DeleteData(table: "data_types", keyColumn: "id", keyValue: 1);
            m.DeleteData(table: "data_types", keyColumn: "id", keyValue: 2);
            m.DeleteData(table: "data_types", keyColumn: "id", keyValue: 3);
            m.DeleteData(table: "data_types", keyColumn: "id", keyValue: 4);
        }
    }
}
