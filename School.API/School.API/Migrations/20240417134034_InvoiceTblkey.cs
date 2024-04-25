using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceTblkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "InvoiceGenerates",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceGenerates",
                table: "InvoiceGenerates",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceGenerates",
                table: "InvoiceGenerates");

            migrationBuilder.DropColumn(
                name: "id",
                table: "InvoiceGenerates");
        }
    }
}
