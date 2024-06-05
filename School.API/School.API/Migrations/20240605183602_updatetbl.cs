using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class updatetbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClassTeacher",
                table: "ClassWiseSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "ClassWiseSubjects");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TeacherDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "ClassWiseSubjects",
                newName: "SubjectId");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectTeacherId",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Classesid",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherDetailsId",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "academicYearId",
                table: "ClassAssignSubjectTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClassWiseSubjects_SubjectId",
                table: "ClassWiseSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAssignSubjectTeachers_Classesid",
                table: "ClassAssignSubjectTeachers",
                column: "Classesid");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAssignSubjectTeachers_SectionId",
                table: "ClassAssignSubjectTeachers",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAssignSubjectTeachers_SubjectId",
                table: "ClassAssignSubjectTeachers",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAssignSubjectTeachers_TeacherDetailsId",
                table: "ClassAssignSubjectTeachers",
                column: "TeacherDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAssignSubjectTeachers_Subjects_SubjectId",
                table: "ClassAssignSubjectTeachers",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAssignSubjectTeachers_TeacherDetails_TeacherDetailsId",
                table: "ClassAssignSubjectTeachers",
                column: "TeacherDetailsId",
                principalTable: "TeacherDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAssignSubjectTeachers_classes_Classesid",
                table: "ClassAssignSubjectTeachers",
                column: "Classesid",
                principalTable: "classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAssignSubjectTeachers_section_SectionId",
                table: "ClassAssignSubjectTeachers",
                column: "SectionId",
                principalTable: "section",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassWiseSubjects_Subjects_SubjectId",
                table: "ClassWiseSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassAssignSubjectTeachers_Subjects_SubjectId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAssignSubjectTeachers_TeacherDetails_TeacherDetailsId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAssignSubjectTeachers_classes_Classesid",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAssignSubjectTeachers_section_SectionId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassWiseSubjects_Subjects_SubjectId",
                table: "ClassWiseSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassWiseSubjects_SubjectId",
                table: "ClassWiseSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassAssignSubjectTeachers_Classesid",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ClassAssignSubjectTeachers_SectionId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ClassAssignSubjectTeachers_SubjectId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ClassAssignSubjectTeachers_TeacherDetailsId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "Classesid",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "TeacherDetailsId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.DropColumn(
                name: "academicYearId",
                table: "ClassAssignSubjectTeachers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TeacherDetails",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "ClassWiseSubjects",
                newName: "Subject");

            migrationBuilder.AddColumn<bool>(
                name: "IsClassTeacher",
                table: "ClassWiseSubjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "ClassWiseSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectTeacherId",
                table: "ClassAssignSubjectTeachers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "ClassAssignSubjectTeachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "ClassAssignSubjectTeachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ClassAssignSubjectTeachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
