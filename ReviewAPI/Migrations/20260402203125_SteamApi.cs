using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class SteamApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngestionStates");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnriched",
                table: "SteamApps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "SteamApps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Linux",
                table: "SteamApps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Mac",
                table: "SteamApps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequiredAge",
                table: "SteamApps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "SteamApps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Windows",
                table: "SteamApps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SteamAppCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamAppCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "SteamAppToCategory",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamAppToCategory", x => new { x.AppId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_SteamAppToCategory_SteamAppCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "SteamAppCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SteamAppToCategory_SteamApps_AppId",
                        column: x => x.AppId,
                        principalTable: "SteamApps",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SteamAppToCategory_CategoryId",
                table: "SteamAppToCategory",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SteamAppToCategory");

            migrationBuilder.DropTable(
                name: "SteamAppCategory");

            migrationBuilder.DropColumn(
                name: "IsEnriched",
                table: "SteamApps");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "SteamApps");

            migrationBuilder.DropColumn(
                name: "Linux",
                table: "SteamApps");

            migrationBuilder.DropColumn(
                name: "Mac",
                table: "SteamApps");

            migrationBuilder.DropColumn(
                name: "RequiredAge",
                table: "SteamApps");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "SteamApps");

            migrationBuilder.DropColumn(
                name: "Windows",
                table: "SteamApps");

            migrationBuilder.CreateTable(
                name: "IngestionStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastAppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngestionStates", x => x.Id);
                });
        }
    }
}
