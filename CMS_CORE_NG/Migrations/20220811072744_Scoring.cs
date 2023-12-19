using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class Scoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Buisness",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreStatus",
                table: "Buisness",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ScoringActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(nullable: true),
                    ActionDescribe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoringEventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringEventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScroingOperations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<int>(nullable: false),
                    EventTypeId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: true),
                    TillDate = table.Column<DateTime>(nullable: true),
                    ScoringActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScroingOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScroingOperations_ScoringEventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "ScoringEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScroingOperations_ScoringActions_ScoringActionId",
                        column: x => x.ScoringActionId,
                        principalTable: "ScoringActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessScorings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(nullable: false),
                    ScoringOperationId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessScorings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessScorings_Buisness_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessScorings_ScroingOperations_ScoringOperationId",
                        column: x => x.ScoringOperationId,
                        principalTable: "ScroingOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessScorings_BusinessId",
                table: "BusinessScorings",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessScorings_ScoringOperationId",
                table: "BusinessScorings",
                column: "ScoringOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScroingOperations_EventTypeId",
                table: "ScroingOperations",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScroingOperations_ScoringActionId",
                table: "ScroingOperations",
                column: "ScoringActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessScorings");

            migrationBuilder.DropTable(
                name: "ScroingOperations");

            migrationBuilder.DropTable(
                name: "ScoringEventTypes");

            migrationBuilder.DropTable(
                name: "ScoringActions");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Buisness");

            migrationBuilder.DropColumn(
                name: "ScoreStatus",
                table: "Buisness");
        }
    }
}
