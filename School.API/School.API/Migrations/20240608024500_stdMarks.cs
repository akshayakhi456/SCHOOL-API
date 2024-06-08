using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class stdMarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamName",
                table: "StudentMarks");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "StudentMarks");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "StudentMarks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "StudentMarks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "StudentMarks");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "StudentMarks");

            migrationBuilder.AddColumn<string>(
                name: "ExamName",
                table: "StudentMarks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "StudentMarks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
