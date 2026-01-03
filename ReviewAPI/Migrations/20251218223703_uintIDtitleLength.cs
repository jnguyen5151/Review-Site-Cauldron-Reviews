using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class uintIDtitleLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop PK to allow altering ReviewId
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews");

            // Alter other columns
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "GameReviews",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<long>(
                name: "Likes",
                table: "GameReviews",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Dislikes",
                table: "GameReviews",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CommentNumber",
                table: "GameReviews",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Alter primary key column
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop PK to revert ReviewId
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews");

            // Revert column changes
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "GameReviews",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                table: "GameReviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Dislikes",
                table: "GameReviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CommentNumber",
                table: "GameReviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
    }
}
