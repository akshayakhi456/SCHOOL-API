using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTblFK1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentAllotmentId",
                table: "Payments",
                newName: "PaymentAllotment");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentAllotment",
                table: "Payments",
                column: "PaymentAllotment");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_paymentAllotments_PaymentAllotment",
                table: "Payments",
                column: "PaymentAllotment",
                principalTable: "paymentAllotments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_paymentAllotments_PaymentAllotment",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentAllotment",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentAllotment",
                table: "Payments",
                newName: "PaymentAllotmentId");
        }
    }
}
