using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ReviewAPI.Migrations.SteamApps
{
    public partial class SteamAppsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SteamApps",
                columns: table => new
                {
                    AppId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<string>(nullable: true),
                    HeaderImage = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamApps", x => x.AppId);
                });

            migrationBuilder.CreateTable(
                name: "SteamAppToGenre",
                columns: table => new
                {
                    SteamAppId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamAppToGenre", x => new { x.SteamAppId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_SteamAppToGenre_SteamApps_SteamAppId",
                        column: x => x.SteamAppId,
                        principalTable: "SteamApps",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Repeat similar blocks for SteamAppToDeveloper and SteamAppToPublisher
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SteamAppToGenre");
            migrationBuilder.DropTable(name: "SteamAppToDeveloper");
            migrationBuilder.DropTable(name: "SteamAppToPublisher");
            migrationBuilder.DropTable(name: "SteamApps");
        }
    }
}