using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class updatetblColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "StudentClassSections");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "StudentClassSections");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "StudentClassSections");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "StudentClassSections");

            migrationBuilder.DropColumn(
                name: "SId",
                table: "StudentClassSections");

            migrationBuilder.RenameColumn(
                name: "Section",
                table: "StudentClassSections",
                newName: "StudentsId");

            migrationBuilder.RenameColumn(
                name: "AcademicYear",
                table: "StudentClassSections",
                newName: "SectionId");

            migrationBuilder.AddColumn<int>(
                name: "AcademicYearId",
                table: "StudentClassSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "StudentClassSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentMarks_SubjectId",
                table: "StudentMarks",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentMarks_Subjects_SubjectId",
                table: "StudentMarks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentMarks_Subjects_SubjectId",
                table: "StudentMarks");

            migrationBuilder.DropIndex(
                name: "IX_StudentMarks_SubjectId",
                table: "StudentMarks");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "StudentClassSections");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "StudentClassSections");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "StudentClassSections",
                newName: "Section");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "StudentClassSections",
                newName: "AcademicYear");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "StudentClassSections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "StudentClassSections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "StudentClassSections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "StudentClassSections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SId",
                table: "StudentClassSections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
