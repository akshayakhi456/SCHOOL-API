using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class updatetblColumnFK1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassSections_Students_Studentsid1",
                table: "StudentClassSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassSections_Studentsid1",
                table: "StudentClassSections");

            migrationBuilder.DropColumn(
                name: "Studentsid1",
                table: "StudentClassSections");

            migrationBuilder.AlterColumn<int>(
                name: "Studentsid",
                table: "StudentClassSections",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "studentId",
                table: "Guardians",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassSections_Studentsid",
                table: "StudentClassSections",
                column: "Studentsid");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassSections_Students_Studentsid",
                table: "StudentClassSections",
                column: "Studentsid",
                principalTable: "Students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassSections_Students_Studentsid",
                table: "StudentClassSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassSections_Studentsid",
                table: "StudentClassSections");

            migrationBuilder.AlterColumn<string>(
                name: "Studentsid",
                table: "StudentClassSections",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Studentsid1",
                table: "StudentClassSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "studentId",
                table: "Guardians",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
        }
    }
}
