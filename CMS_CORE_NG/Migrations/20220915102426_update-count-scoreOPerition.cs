using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class updatecountscoreOPerition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 9,
                column: "Count",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 10,
                column: "Count",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 11,
                column: "Count",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 9,
                column: "Count",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 10,
                column: "Count",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 11,
                column: "Count",
                value: 3);
        }
    }
}
