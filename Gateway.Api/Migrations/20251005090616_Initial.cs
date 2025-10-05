using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gateway.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_data_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rule_operators",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rule_operators", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rule_versions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rule_key = table.Column<Guid>(type: "uuid", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    display_description = table.Column<string>(type: "text", nullable: true),
                    weight = table.Column<decimal>(type: "numeric", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rule_versions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "value_domains",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_value_domains", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "policy_constants",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_policy_constants", x => x.id);
                    table.ForeignKey(
                        name: "fk_policy_constants_data_types_type_id",
                        column: x => x.type_id,
                        principalTable: "data_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "actual_rules",
                columns: table => new
                {
                    rule_key = table.Column<Guid>(type: "uuid", nullable: false),
                    rule_version_id = table.Column<long>(type: "bigint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_actual_rules", x => x.rule_key);
                    table.ForeignKey(
                        name: "fk_actual_rules_rule_versions_rule_version_id",
                        column: x => x.rule_version_id,
                        principalTable: "rule_versions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rule_fields",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    data_type_id = table.Column<int>(type: "integer", nullable: false),
                    value_domain_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rule_fields", x => x.id);
                    table.ForeignKey(
                        name: "fk_rule_fields_data_types_data_type_id",
                        column: x => x.data_type_id,
                        principalTable: "data_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_rule_fields_value_domains_value_domain_id",
                        column: x => x.value_domain_id,
                        principalTable: "value_domains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "value_domain_values",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value_domain_id = table.Column<int>(type: "integer", nullable: false),
                    data_type_id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    label = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_value_domain_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_value_domain_values_data_types_data_type_id",
                        column: x => x.data_type_id,
                        principalTable: "data_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_value_domain_values_value_domains_value_domain_id",
                        column: x => x.value_domain_id,
                        principalTable: "value_domains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rule_field_operators",
                columns: table => new
                {
                    field_id = table.Column<int>(type: "integer", nullable: false),
                    operator_id = table.Column<int>(type: "integer", nullable: false),
                    constraints = table.Column<string>(type: "text", nullable: true),
                    value_source = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rule_field_operators", x => new { x.field_id, x.operator_id });
                    table.ForeignKey(
                        name: "fk_rule_field_operators_rule_fields_field_id",
                        column: x => x.field_id,
                        principalTable: "rule_fields",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rule_field_operators_rule_operators_operator_id",
                        column: x => x.operator_id,
                        principalTable: "rule_operators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    global_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bank_from = table.Column<string>(type: "text", nullable: false),
                    bank_to = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_transactions_value_domain_values_currency_id",
                        column: x => x.currency_id,
                        principalTable: "value_domain_values",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "transaction_results",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transaction_id = table.Column<long>(type: "bigint", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: false),
                    risk_score = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_results", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_results_transactions_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transaction_results_value_domain_values_currency_id",
                        column: x => x.currency_id,
                        principalTable: "value_domain_values",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rule_evaluations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transaction_result_id = table.Column<long>(type: "bigint", nullable: false),
                    rule_version_id = table.Column<long>(type: "bigint", nullable: false),
                    is_passed = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rule_evaluations", x => x.id);
                    table.ForeignKey(
                        name: "fk_rule_evaluations_rule_versions_rule_version_id",
                        column: x => x.rule_version_id,
                        principalTable: "rule_versions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_rule_evaluations_transaction_results_transaction_result_id",
                        column: x => x.transaction_result_id,
                        principalTable: "transaction_results",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_actual_rules_rule_version_id",
                table: "actual_rules",
                column: "rule_version_id");

            migrationBuilder.CreateIndex(
                name: "ix_data_types_code",
                table: "data_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_policy_constants_code",
                table: "policy_constants",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_policy_constants_type_id",
                table: "policy_constants",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_rule_evaluations_rule_version_id",
                table: "rule_evaluations",
                column: "rule_version_id");

            migrationBuilder.CreateIndex(
                name: "ix_rule_evaluations_transaction_result_id_rule_version_id",
                table: "rule_evaluations",
                columns: new[] { "transaction_result_id", "rule_version_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rule_field_operators_operator_id",
                table: "rule_field_operators",
                column: "operator_id");

            migrationBuilder.CreateIndex(
                name: "ix_rule_fields_data_type_id",
                table: "rule_fields",
                column: "data_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_rule_fields_name",
                table: "rule_fields",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rule_fields_value_domain_id",
                table: "rule_fields",
                column: "value_domain_id");

            migrationBuilder.CreateIndex(
                name: "ix_rule_operators_code",
                table: "rule_operators",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rule_versions_rule_key_version",
                table: "rule_versions",
                columns: new[] { "rule_key", "version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transaction_results_currency_id",
                table: "transaction_results",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_results_transaction_id",
                table: "transaction_results",
                column: "transaction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transactions_currency_id",
                table: "transactions",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_global_id",
                table: "transactions",
                column: "global_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_value_domain_values_data_type_id",
                table: "value_domain_values",
                column: "data_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_value_domain_values_value_domain_id_code",
                table: "value_domain_values",
                columns: new[] { "value_domain_id", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_value_domains_code",
                table: "value_domains",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actual_rules");

            migrationBuilder.DropTable(
                name: "policy_constants");

            migrationBuilder.DropTable(
                name: "rule_evaluations");

            migrationBuilder.DropTable(
                name: "rule_field_operators");

            migrationBuilder.DropTable(
                name: "rule_versions");

            migrationBuilder.DropTable(
                name: "transaction_results");

            migrationBuilder.DropTable(
                name: "rule_fields");

            migrationBuilder.DropTable(
                name: "rule_operators");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "value_domain_values");

            migrationBuilder.DropTable(
                name: "data_types");

            migrationBuilder.DropTable(
                name: "value_domains");
        }
    }
}
