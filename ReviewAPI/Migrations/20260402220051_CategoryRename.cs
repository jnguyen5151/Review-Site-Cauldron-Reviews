using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class CategoryRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
            {
                CategoryId = table.Column<int>(type: "int", nullable: false),
                Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.CategoryId);
            });

            migrationBuilder.CreateTable(
                name: "AppCategories",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategories", x => new { x.AppId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_AppCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCategories_SteamApps_AppId",
                        column: x => x.AppId,
                        principalTable: "SteamApps",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCategories_CategoryId",
                table: "AppCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCategories_Categories_CategoryId",
                table: "AppCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_AppCategories_SteamApps_AppId",
                table: "AppCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCategories",
                table: "AppCategories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "SteamAppCategory");

            migrationBuilder.RenameTable(
                name: "AppCategories",
                newName: "SteamAppToCategory");

            migrationBuilder.RenameIndex(
                name: "IX_AppCategories_CategoryId",
                table: "SteamAppToCategory",
                newName: "IX_SteamAppToCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SteamAppCategory",
                table: "SteamAppCategory",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SteamAppToCategory",
                table: "SteamAppToCategory",
                columns: new[] { "AppId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SteamAppToCategory_SteamAppCategory_CategoryId",
                table: "SteamAppToCategory",
                column: "CategoryId",
                principalTable: "SteamAppCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SteamAppToCategory_SteamApps_AppId",
                table: "SteamAppToCategory",
                column: "AppId",
                principalTable: "SteamApps",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
