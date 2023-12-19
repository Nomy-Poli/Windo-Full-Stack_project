using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class Networkinggroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Notes",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NetworkingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true),
                    ManagerBusinessId = table.Column<int>(nullable: true),
                    ManagerBusinessEmail = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    AreaId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkingGroups_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NetworkingGroups_Buisness_ManagerBusinessId",
                        column: x => x.ManagerBusinessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "networkingGroupBusinesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_networkingGroupBusinesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_networkingGroupBusinesses_Buisness_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_networkingGroupBusinesses_NetworkingGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "NetworkingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_GroupId",
                table: "Notes",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_networkingGroupBusinesses_BusinessId",
                table: "networkingGroupBusinesses",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_networkingGroupBusinesses_GroupId",
                table: "networkingGroupBusinesses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkingGroups_AreaId",
                table: "NetworkingGroups",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkingGroups_ManagerBusinessId",
                table: "NetworkingGroups",
                column: "ManagerBusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NetworkingGroups_GroupId",
                table: "Notes",
                column: "GroupId",
                principalTable: "NetworkingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NetworkingGroups_GroupId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "networkingGroupBusinesses");

            migrationBuilder.DropTable(
                name: "NetworkingGroups");

            migrationBuilder.DropIndex(
                name: "IX_Notes_GroupId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Notes");
        }
    }
}
