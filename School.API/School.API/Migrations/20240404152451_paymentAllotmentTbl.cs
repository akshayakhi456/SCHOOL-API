using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class paymentAllotmentTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "feeName",
                table: "Payments",
                newName: "paymentName");

            migrationBuilder.CreateTable(
                name: "paymentAllotments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    className = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentAllotments", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "paymentAllotments");

            migrationBuilder.RenameColumn(
                name: "paymentName",
                table: "Payments",
                newName: "feeName");
        }
    }
}
