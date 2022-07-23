using Microsoft.EntityFrameworkCore.Migrations;

namespace JeddahSnipers.Migrations
{
    public partial class removedAttendanceFromStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceStudent");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AttendanceId",
                table: "Students",
                column: "AttendanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Attendances_AttendanceId",
                table: "Students",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "AttendanceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Attendances_AttendanceId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AttendanceId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "AttendanceStudent",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceStudent", x => new { x.AttendanceId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_AttendanceStudent_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "AttendanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceStudent_StudentId",
                table: "AttendanceStudent",
                column: "StudentId");
        }
    }
}
