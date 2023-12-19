using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class AddfieldbuisnessNametoNetworkingGroupBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses");

            migrationBuilder.AddColumn<string>(
                name: "buisnessName",
                table: "networkingGroupBusinesses",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: "#47caeb");

            migrationBuilder.CreateIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses");

            migrationBuilder.DropColumn(
                name: "buisnessName",
                table: "networkingGroupBusinesses");

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: "#47CAEB");

            migrationBuilder.CreateIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses",
                column: "BusinessId",
                unique: true,
                filter: "[BusinessId] IS NOT NULL");
        }
    }
}
