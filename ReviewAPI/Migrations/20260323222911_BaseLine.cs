using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class BaseLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.DeveloperId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

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

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "SteamApps",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamApps", x => x.AppId);
                });

            migrationBuilder.CreateTable(
                name: "AppDevelopers",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false),
                    DeveloperId = table.Column<int>(type: "int", nullable: false),
                    SteamAppAppId = table.Column<int>(type: "int", nullable: false),
                    SteamAppDeveloperDeveloperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDevelopers", x => new { x.AppId, x.DeveloperId });
                    table.ForeignKey(
                        name: "FK_AppDevelopers_Developers_SteamAppDeveloperDeveloperId",
                        column: x => x.SteamAppDeveloperDeveloperId,
                        principalTable: "Developers",
                        principalColumn: "DeveloperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDevelopers_SteamApps_SteamAppAppId",
                        column: x => x.SteamAppAppId,
                        principalTable: "SteamApps",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppGenres",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    SteamAppAppId = table.Column<int>(type: "int", nullable: false),
                    SteamAppGenreGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGenres", x => new { x.AppId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_AppGenres_Genres_SteamAppGenreGenreId",
                        column: x => x.SteamAppGenreGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppGenres_SteamApps_SteamAppAppId",
                        column: x => x.SteamAppAppId,
                        principalTable: "SteamApps",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPublishers",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    SteamAppAppId = table.Column<int>(type: "int", nullable: false),
                    SteamAppPublisherPublisherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPublishers", x => new { x.AppId, x.PublisherId });
                    table.ForeignKey(
                        name: "FK_AppPublishers_publishers_SteamAppPublisherPublisherId",
                        column: x => x.SteamAppPublisherPublisherId,
                        principalTable: "publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPublishers_SteamApps_SteamAppAppId",
                        column: x => x.SteamAppAppId,
                        principalTable: "SteamApps",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDevelopers_SteamAppAppId",
                table: "AppDevelopers",
                column: "SteamAppAppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDevelopers_SteamAppDeveloperDeveloperId",
                table: "AppDevelopers",
                column: "SteamAppDeveloperDeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_AppGenres_SteamAppAppId",
                table: "AppGenres",
                column: "SteamAppAppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppGenres_SteamAppGenreGenreId",
                table: "AppGenres",
                column: "SteamAppGenreGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPublishers_SteamAppAppId",
                table: "AppPublishers",
                column: "SteamAppAppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPublishers_SteamAppPublisherPublisherId",
                table: "AppPublishers",
                column: "SteamAppPublisherPublisherId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDevelopers");

            migrationBuilder.DropTable(
                name: "AppGenres");

            migrationBuilder.DropTable(
                name: "AppPublishers");

            migrationBuilder.DropTable(
                name: "IngestionStates");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "publishers");

            migrationBuilder.DropTable(
                name: "SteamApps");

        }
    }
}
