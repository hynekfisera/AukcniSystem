using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AukcniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ukoncena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ukoncena",
                table: "Aukce",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ukoncena",
                table: "Aukce");
        }
    }
}
