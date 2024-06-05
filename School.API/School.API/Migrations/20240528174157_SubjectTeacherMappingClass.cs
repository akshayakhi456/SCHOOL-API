using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class SubjectTeacherMappingClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Classesid",
                table: "SubjectExamMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectExamMappings_Classesid",
                table: "SubjectExamMappings",
                column: "Classesid");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectExamMappings_classes_Classesid",
                table: "SubjectExamMappings",
                column: "Classesid",
                principalTable: "classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectExamMappings_classes_Classesid",
                table: "SubjectExamMappings");

            migrationBuilder.DropIndex(
                name: "IX_SubjectExamMappings_Classesid",
                table: "SubjectExamMappings");

            migrationBuilder.DropColumn(
                name: "Classesid",
                table: "SubjectExamMappings");
        }
    }
}
