using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrinkBuddy.Migrations
{
    /// <inheritdoc />
    public partial class Profile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Beschreibung",
                table: "ProfilTable",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FavorisierterDrinkTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProfilTableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavorisierterDrinkTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavorisierterDrinkTable_ProfilTable_ProfilTableId",
                        column: x => x.ProfilTableId,
                        principalTable: "ProfilTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavorisierterDrinkTable_ProfilTableId",
                table: "FavorisierterDrinkTable",
                column: "ProfilTableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavorisierterDrinkTable");

            migrationBuilder.DropColumn(
                name: "Beschreibung",
                table: "ProfilTable");
        }
    }
}
