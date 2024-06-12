using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class updatetblColumnFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "StudentClassSections",
                newName: "Studentsid");

            migrationBuilder.AddColumn<int>(
                name: "Studentsid1",
                table: "StudentClassSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassSections_SectionId",
                table: "StudentClassSections",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassSections_Studentsid1",
                table: "StudentClassSections",
                column: "Studentsid1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassSections_Students_Studentsid1",
                table: "StudentClassSections",
                column: "Studentsid1",
                principalTable: "Students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassSections_section_SectionId",
                table: "StudentClassSections",
                column: "SectionId",
                principalTable: "section",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassSections_Students_Studentsid1",
                table: "StudentClassSections");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassSections_section_SectionId",
                table: "StudentClassSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassSections_SectionId",
                table: "StudentClassSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassSections_Studentsid1",
                table: "StudentClassSections");

            migrationBuilder.DropColumn(
                name: "Studentsid1",
                table: "StudentClassSections");

            migrationBuilder.RenameColumn(
                name: "Studentsid",
                table: "StudentClassSections",
                newName: "StudentsId");
        }
    }
}
