using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class AdvertismentsContactName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "RequsetsOrderService");

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "RequsetsOrderService",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "RequsetsOrderService",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "RequsetsOrderService");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "RequsetsOrderService");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RequsetsOrderService",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
