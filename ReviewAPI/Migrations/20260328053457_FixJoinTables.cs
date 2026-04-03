using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixJoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDevelopers_Developers_SteamAppDeveloperDeveloperId",
                table: "AppDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDevelopers_SteamApps_SteamAppAppId",
                table: "AppDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppGenres_Genres_SteamAppGenreGenreId",
                table: "AppGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_AppGenres_SteamApps_SteamAppAppId",
                table: "AppGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPublishers_SteamApps_SteamAppAppId",
                table: "AppPublishers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPublishers_Publishers_SteamAppPublisherPublisherId",
                table: "AppPublishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                table: "publishers");

            migrationBuilder.DropIndex(
                name: "IX_AppPublishers_SteamAppAppId",
                table: "AppPublishers");

            migrationBuilder.DropIndex(
                name: "IX_AppPublishers_SteamAppPublisherPublisherId",
                table: "AppPublishers");

            migrationBuilder.DropIndex(
                name: "IX_AppGenres_SteamAppAppId",
                table: "AppGenres");

            migrationBuilder.DropIndex(
                name: "IX_AppGenres_SteamAppGenreGenreId",
                table: "AppGenres");

            migrationBuilder.DropIndex(
                name: "IX_AppDevelopers_SteamAppAppId",
                table: "AppDevelopers");

            migrationBuilder.DropIndex(
                name: "IX_AppDevelopers_SteamAppDeveloperDeveloperId",
                table: "AppDevelopers");

            migrationBuilder.DropColumn(
                name: "SteamAppAppId",
                table: "AppPublishers");

            migrationBuilder.DropColumn(
                name: "SteamAppPublisherPublisherId",
                table: "AppPublishers");

            migrationBuilder.DropColumn(
                name: "SteamAppAppId",
                table: "AppGenres");

            migrationBuilder.DropColumn(
                name: "SteamAppGenreGenreId",
                table: "AppGenres");

            migrationBuilder.DropColumn(
                name: "SteamAppAppId",
                table: "AppDevelopers");

            migrationBuilder.DropColumn(
                name: "SteamAppDeveloperDeveloperId",
                table: "AppDevelopers");

            migrationBuilder.RenameTable(
                name: "publishers",
                newName: "Publishers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPublishers_PublisherId",
                table: "AppPublishers",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_AppGenres_GenreId",
                table: "AppGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDevelopers_DeveloperId",
                table: "AppDevelopers",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDevelopers_Developers_DeveloperId",
                table: "AppDevelopers",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "DeveloperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDevelopers_SteamApps_AppId",
                table: "AppDevelopers",
                column: "AppId",
                principalTable: "SteamApps",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppGenres_Genres_GenreId",
                table: "AppGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppGenres_SteamApps_AppId",
                table: "AppGenres",
                column: "AppId",
                principalTable: "SteamApps",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPublishers_Publishers_PublisherId",
                table: "AppPublishers",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPublishers_SteamApps_AppId",
                table: "AppPublishers",
                column: "AppId",
                principalTable: "SteamApps",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDevelopers_Developers_DeveloperId",
                table: "AppDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDevelopers_SteamApps_AppId",
                table: "AppDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppGenres_Genres_GenreId",
                table: "AppGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_AppGenres_SteamApps_AppId",
                table: "AppGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPublishers_Publishers_PublisherId",
                table: "AppPublishers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPublishers_SteamApps_AppId",
                table: "AppPublishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_AppPublishers_PublisherId",
                table: "AppPublishers");

            migrationBuilder.DropIndex(
                name: "IX_AppGenres_GenreId",
                table: "AppGenres");

            migrationBuilder.DropIndex(
                name: "IX_AppDevelopers_DeveloperId",
                table: "AppDevelopers");

            migrationBuilder.RenameTable(
                name: "Publishers",
                newName: "publishers");

            migrationBuilder.AddColumn<int>(
                name: "SteamAppAppId",
                table: "AppPublishers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SteamAppPublisherPublisherId",
                table: "AppPublishers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SteamAppAppId",
                table: "AppGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SteamAppGenreGenreId",
                table: "AppGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SteamAppAppId",
                table: "AppDevelopers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SteamAppDeveloperDeveloperId",
                table: "AppDevelopers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                table: "publishers",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPublishers_SteamAppAppId",
                table: "AppPublishers",
                column: "SteamAppAppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPublishers_SteamAppPublisherPublisherId",
                table: "AppPublishers",
                column: "SteamAppPublisherPublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_AppGenres_SteamAppAppId",
                table: "AppGenres",
                column: "SteamAppAppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppGenres_SteamAppGenreGenreId",
                table: "AppGenres",
                column: "SteamAppGenreGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDevelopers_SteamAppAppId",
                table: "AppDevelopers",
                column: "SteamAppAppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDevelopers_SteamAppDeveloperDeveloperId",
                table: "AppDevelopers",
                column: "SteamAppDeveloperDeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDevelopers_Developers_SteamAppDeveloperDeveloperId",
                table: "AppDevelopers",
                column: "SteamAppDeveloperDeveloperId",
                principalTable: "Developers",
                principalColumn: "DeveloperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDevelopers_SteamApps_SteamAppAppId",
                table: "AppDevelopers",
                column: "SteamAppAppId",
                principalTable: "SteamApps",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppGenres_Genres_SteamAppGenreGenreId",
                table: "AppGenres",
                column: "SteamAppGenreGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppGenres_SteamApps_SteamAppAppId",
                table: "AppGenres",
                column: "SteamAppAppId",
                principalTable: "SteamApps",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPublishers_SteamApps_SteamAppAppId",
                table: "AppPublishers",
                column: "SteamAppAppId",
                principalTable: "SteamApps",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPublishers_publishers_SteamAppPublisherPublisherId",
                table: "AppPublishers",
                column: "SteamAppPublisherPublisherId",
                principalTable: "publishers",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
