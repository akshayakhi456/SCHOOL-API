using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class examrelatedTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectExamMappings");

            migrationBuilder.CreateTable(
                name: "ClassWiseSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    IsClassTeacher = table.Column<bool>(type: "bit", nullable: true),
                    academicYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassWiseSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamSubjectSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    MinMarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxMarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAddInTotal = table.Column<bool>(type: "bit", nullable: false),
                    WillExamConduct = table.Column<bool>(type: "bit", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    academicYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSubjectSchedules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassWiseSubjects");

            migrationBuilder.DropTable(
                name: "ExamSubjectSchedules");

            migrationBuilder.CreateTable(
                name: "SubjectExamMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Classesid = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectExamMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectExamMappings_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectExamMappings_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectExamMappings_classes_Classesid",
                        column: x => x.Classesid,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectExamMappings_Classesid",
                table: "SubjectExamMappings",
                column: "Classesid");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectExamMappings_ExamId",
                table: "SubjectExamMappings",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectExamMappings_SubjectId",
                table: "SubjectExamMappings",
                column: "SubjectId");
        }
    }
}
