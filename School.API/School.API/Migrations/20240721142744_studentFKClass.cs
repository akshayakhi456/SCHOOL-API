using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class studentFKClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "className",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "className",
                table: "paymentAllotments");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentClassName",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "classesId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "classId",
                table: "paymentAllotments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_classesId",
                table: "Students",
                column: "classesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_classes_classesId",
                table: "Students",
                column: "classesId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_classes_classesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_classesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "classesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "classId",
                table: "paymentAllotments");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentClassName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "className",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "className",
                table: "paymentAllotments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
