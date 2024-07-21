﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.API.Migrations
{
    /// <inheritdoc />
    public partial class examTimeInExamRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExamTime",
                table: "ExamSubjectSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamTime",
                table: "ExamSubjectSchedules");
        }
    }
}
