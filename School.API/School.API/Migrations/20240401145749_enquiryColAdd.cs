using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class enquiryColAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "parentInteraction",
                table: "Enquiry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "Enquiry",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "review",
                table: "Enquiry",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parentInteraction",
                table: "Enquiry");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "Enquiry");

            migrationBuilder.DropColumn(
                name: "review",
                table: "Enquiry");
        }
    }
}
