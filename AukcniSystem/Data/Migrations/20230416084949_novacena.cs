using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AukcniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class novacena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "NovaCena",
                table: "Prihozy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NovaCena",
                table: "Prihozy");
        }
    }
}
