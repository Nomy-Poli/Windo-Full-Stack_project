using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class AddfieldISActivtoNetworkingGrouptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NetworkingGroups_GroupId",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Notes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NetworkingGroups",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NetworkingGroups_GroupId",
                table: "Notes",
                column: "GroupId",
                principalTable: "NetworkingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NetworkingGroups_GroupId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NetworkingGroups");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Notes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NetworkingGroups_GroupId",
                table: "Notes",
                column: "GroupId",
                principalTable: "NetworkingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
