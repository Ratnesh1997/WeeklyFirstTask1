using Microsoft.EntityFrameworkCore.Migrations;

namespace WeeklyFirstTask.Migrations
{
    public partial class a10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GetDateTime",
                table: "GetDoctors",
                newName: "GetTime");

            migrationBuilder.AddColumn<string>(
                name: "GetDate",
                table: "GetDoctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetDate",
                table: "GetDoctors");

            migrationBuilder.RenameColumn(
                name: "GetTime",
                table: "GetDoctors",
                newName: "GetDateTime");
        }
    }
}
