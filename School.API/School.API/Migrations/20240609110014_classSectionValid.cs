using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class classSectionValid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "className",
                table: "section");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "classes",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ClassesId",
                table: "section",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_section_ClassesId",
                table: "section",
                column: "ClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_section_classes_ClassesId",
                table: "section",
                column: "ClassesId",
                principalTable: "classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_section_classes_ClassesId",
                table: "section");

            migrationBuilder.DropIndex(
                name: "IX_section_ClassesId",
                table: "section");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "section");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "classes",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "className",
                table: "section",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
