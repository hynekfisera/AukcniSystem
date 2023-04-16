using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AukcniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class nevimuz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aukce_AspNetUsers_AutorId",
                table: "Aukce");

            migrationBuilder.AlterColumn<string>(
                name: "AutorId",
                table: "Aukce",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Aukce_AspNetUsers_AutorId",
                table: "Aukce",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aukce_AspNetUsers_AutorId",
                table: "Aukce");

            migrationBuilder.AlterColumn<string>(
                name: "AutorId",
                table: "Aukce",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aukce_AspNetUsers_AutorId",
                table: "Aukce",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
