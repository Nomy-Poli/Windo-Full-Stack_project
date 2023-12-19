using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class addRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInCollaborations_JointProjects_JointProjectId",
                table: "BusinessInCollaborations");

            migrationBuilder.DropIndex(
                name: "IX_BusinessInCollaborations_JointProjectId",
                table: "BusinessInCollaborations");

            migrationBuilder.DropColumn(
                name: "JointProjectId",
                table: "BusinessInCollaborations");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInCollaborations_BusinessId",
                table: "BusinessInCollaborations",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInCollaborations_JoinProjectId",
                table: "BusinessInCollaborations",
                column: "JoinProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInCollaborations_Buisness_BusinessId",
                table: "BusinessInCollaborations",
                column: "BusinessId",
                principalTable: "Buisness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInCollaborations_JointProjects_JoinProjectId",
                table: "BusinessInCollaborations",
                column: "JoinProjectId",
                principalTable: "JointProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInCollaborations_Buisness_BusinessId",
                table: "BusinessInCollaborations");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInCollaborations_JointProjects_JoinProjectId",
                table: "BusinessInCollaborations");

            migrationBuilder.DropIndex(
                name: "IX_BusinessInCollaborations_BusinessId",
                table: "BusinessInCollaborations");

            migrationBuilder.DropIndex(
                name: "IX_BusinessInCollaborations_JoinProjectId",
                table: "BusinessInCollaborations");

            migrationBuilder.AddColumn<int>(
                name: "JointProjectId",
                table: "BusinessInCollaborations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInCollaborations_JointProjectId",
                table: "BusinessInCollaborations",
                column: "JointProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInCollaborations_JointProjects_JointProjectId",
                table: "BusinessInCollaborations",
                column: "JointProjectId",
                principalTable: "JointProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
