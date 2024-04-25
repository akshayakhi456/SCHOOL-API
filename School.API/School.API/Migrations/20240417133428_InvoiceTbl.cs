using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceGenerates",
                columns: table => new
                {
                    invoiceId = table.Column<int>(type: "int", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    invoicePhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceGenerates");
        }
    }
}
