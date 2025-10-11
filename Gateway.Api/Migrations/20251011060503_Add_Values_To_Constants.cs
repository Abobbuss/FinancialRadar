using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gateway.Api.Migrations
{
    /// <inheritdoc />
    public partial class Add_Values_To_Constants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "policy_constants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "policy_constants");
        }
    }
}
