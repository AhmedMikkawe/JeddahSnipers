using Microsoft.EntityFrameworkCore.Migrations;

namespace JeddahSnipers.Migrations
{
    public partial class addedAgeToGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Age",
                table: "Groups",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Groups");
        }
    }
}
