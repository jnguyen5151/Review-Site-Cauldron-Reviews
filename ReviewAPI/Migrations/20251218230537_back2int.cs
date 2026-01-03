using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class back2int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop PK to allow altering ReviewId
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews");

            // Alter ReviewId column
            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "GameReviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            // Re-add PK
            migrationBuilder.AddPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews",
                column: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop PK to revert ReviewId
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews");

            // Revert column back to long
            migrationBuilder.AlterColumn<long>(
                name: "ReviewId",
                table: "GameReviews",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            // Re-add PK
            migrationBuilder.AddPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews",
                column: "ReviewId");
        }
    }
}
