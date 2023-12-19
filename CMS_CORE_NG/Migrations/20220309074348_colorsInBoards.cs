using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class colorsInBoards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Color",
                value: "#f16b7e");

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Color",
                value: "#f9af74");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Color",
                value: null);
        }
    }
}
