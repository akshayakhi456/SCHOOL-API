using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class classTeacherclassesMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassAssignSubjectTeachers_classes_Classesid",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "SubjectTeacherId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.RenameColumn(
                name: "Classesid",
                table: "ClassAssignSubjectTeachers",
                newName: "ClassesId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassAssignSubjectTeachers_Classesid",
                table: "ClassAssignSubjectTeachers",
                newName: "IX_ClassAssignSubjectTeachers_ClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAssignSubjectTeachers_classes_ClassesId",
                table: "ClassAssignSubjectTeachers",
                column: "ClassesId",
                principalTable: "classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassAssignSubjectTeachers_classes_ClassesId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.RenameColumn(
                name: "ClassesId",
                table: "ClassAssignSubjectTeachers",
                newName: "Classesid");

            migrationBuilder.RenameIndex(
                name: "IX_ClassAssignSubjectTeachers_ClassesId",
                table: "ClassAssignSubjectTeachers",
                newName: "IX_ClassAssignSubjectTeachers_Classesid");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectTeacherId",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAssignSubjectTeachers_classes_Classesid",
                table: "ClassAssignSubjectTeachers",
                column: "Classesid",
                principalTable: "classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
