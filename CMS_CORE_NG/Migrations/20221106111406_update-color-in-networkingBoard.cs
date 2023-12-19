using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class updatecolorinnetworkingBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: "#47caeb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: "47CAEB");
        }
    }
}
