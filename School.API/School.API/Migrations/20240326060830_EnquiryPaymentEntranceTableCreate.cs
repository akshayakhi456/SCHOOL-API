using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class EnquiryPaymentEntranceTableCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnquiryEntranceExams",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateOfExam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modeOfExam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    scheduleTimeForExam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    enquiryStudentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryEntranceExams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paymentsEnquiry",
                columns: table => new
                {
                    invoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfPayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studentEnquireId = table.Column<int>(type: "int", nullable: false),
                    paymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentsEnquiry", x => x.invoiceId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnquiryEntranceExams");

            migrationBuilder.DropTable(
                name: "paymentsEnquiry");
        }
    }
}
