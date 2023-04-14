using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AukcniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class pridatnazev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nazev",
                table: "Aukce",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nazev",
                table: "Aukce");
        }
    }
}
