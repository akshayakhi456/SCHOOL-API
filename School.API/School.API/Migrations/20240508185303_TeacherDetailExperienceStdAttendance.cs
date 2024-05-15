using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class TeacherDetailExperienceStdAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassAssignSubjectTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectTeacherId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClassTeacher = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAssignSubjectTeachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RollNo = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D9 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D10 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D11 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D12 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D13 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D14 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D15 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D16 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D17 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D18 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D19 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D20 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D21 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D22 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D23 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D24 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D25 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D26 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D27 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D28 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D29 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D30 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D31 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassOutYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchooolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartEndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherExperiences", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassAssignSubjectTeachers");

            migrationBuilder.DropTable(
                name: "StudentAttendances");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "TeacherDetails");

            migrationBuilder.DropTable(
                name: "TeacherExperiences");
        }
    }
}
