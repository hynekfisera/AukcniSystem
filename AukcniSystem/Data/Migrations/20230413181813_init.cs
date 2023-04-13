using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AukcniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "CisloPopisne",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mesto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stat",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ulice",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Zustatek",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    KategorieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.KategorieId);
                });

            migrationBuilder.CreateTable(
                name: "Aukce",
                columns: table => new
                {
                    AukceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KategorieId = table.Column<int>(type: "int", nullable: false),
                    Popis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    PrihozeniPoCastce = table.Column<bool>(type: "bit", nullable: false),
                    MinimalniPrihoz = table.Column<double>(type: "float", nullable: false),
                    Schvalena = table.Column<bool>(type: "bit", nullable: false),
                    DobaTrvani = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aukce", x => x.AukceId);
                    table.ForeignKey(
                        name: "FK_Aukce_AspNetUsers_AutorId",
                        column: x => x.AutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aukce_Kategorie_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorie",
                        principalColumn: "KategorieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prihozy",
                columns: table => new
                {
                    PrihozId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Castka = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    KlientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AukceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prihozy", x => x.PrihozId);
                    table.ForeignKey(
                        name: "FK_Prihozy_AspNetUsers_KlientId",
                        column: x => x.KlientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prihozy_Aukce_AukceId",
                        column: x => x.AukceId,
                        principalTable: "Aukce",
                        principalColumn: "AukceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aukce_AutorId",
                table: "Aukce",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Aukce_KategorieId",
                table: "Aukce",
                column: "KategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Prihozy_AukceId",
                table: "Prihozy",
                column: "AukceId");

            migrationBuilder.CreateIndex(
                name: "IX_Prihozy_KlientId",
                table: "Prihozy",
                column: "KlientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prihozy");

            migrationBuilder.DropTable(
                name: "Aukce");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropColumn(
                name: "CisloPopisne",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Mesto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Stat",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ulice",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Zustatek",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
