using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class AddnetworkingBoardtoboardDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses");

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Color", "Description", "Name" },
                values: new object[] { 3, "47CAEB", "כאן תראי מודעות הקשורות לנטוורקינג", "נטוורקינג" });

            migrationBuilder.CreateIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses",
                column: "BusinessId",
                unique: true,
                filter: "[BusinessId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses");

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses",
                column: "BusinessId");
        }
    }
}
