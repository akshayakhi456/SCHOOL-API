using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class studentTableChangesGuardianTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "mobile",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "sibiling",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "guardian",
                table: "Students",
                newName: "gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Students",
                newName: "guardian");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "mobile",
                table: "Students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "sibiling",
                table: "Students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
