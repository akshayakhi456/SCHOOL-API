using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class examfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ExamSubjectSchedules");

            migrationBuilder.RenameColumn(
                name: "academicYearId",
                table: "ExamSubjectSchedules",
                newName: "AcademicYearId");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "ExamSubjectSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSubjectSchedules_SubjectId",
                table: "ExamSubjectSchedules",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSubjectSchedules_Subjects_SubjectId",
                table: "ExamSubjectSchedules",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSubjectSchedules_Subjects_SubjectId",
                table: "ExamSubjectSchedules");

            migrationBuilder.DropIndex(
                name: "IX_ExamSubjectSchedules_SubjectId",
                table: "ExamSubjectSchedules");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ExamSubjectSchedules");

            migrationBuilder.RenameColumn(
                name: "AcademicYearId",
                table: "ExamSubjectSchedules",
                newName: "academicYearId");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ExamSubjectSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
