using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkataAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseEnrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ProgressPercentage = table.Column<int>(type: "integer", nullable: false),
                    ProgressIncrementValue = table.Column<int>(type: "integer", nullable: false),
                    LessonsCompleted = table.Column<int>(type: "integer", nullable: false),
                    ProgressId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressIncrement = table.Column<int>(type: "integer", nullable: false),
                    EnrolledOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActivatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SuspendedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DroppedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrollments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_CourseId",
                table: "CourseEnrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_StudentId",
                table: "CourseEnrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_StudentId_CourseId",
                table: "CourseEnrollments",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEnrollments");
        }
    }
}
