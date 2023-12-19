using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class Scoringtablesandseed : Migration
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
                    EventTypeId = table.Column<int>(nullable: false)
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
                    TillDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScroingOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScroingOperations_ScoringActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "ScoringActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScroingOperations_ScoringEventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "ScoringEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "ScoringActions",
                columns: new[] { "Id", "ActionName", "EventTypeId" },
                values: new object[,]
                {
                    { 1, "הרשמה לאתר", 2 },
                    { 2, "התחברות לאתר", 2 },
                    { 3, "דיווח שת\"פ", 2 },
                    { 4, "פרסום פתקית בלוח המודעות", 2 },
                    { 5, "תגובה לפתקית בלוח", 2 },
                    { 6, "פתיחת כרטיס עסק", 2 },
                    { 7, "הוספת/החלפת קבצי תמונות בכרטיס עסק", 2 },
                    { 8, "הוספת לינק בכרטיס העסק לאתר/תיק עבודות", 2 },
                    { 9, "הקלקה בכרטיס עסק שברשימת העסקים", 2 },
                    { 10, "הקלקה על פתקית בלוח המודעות", 2 },
                    { 11, "הקלקה על קייסטאדי", 2 },
                    { 12, "שליחת הודעה", 2 },
                    { 13, "מענה להודעה", 2 }
                });

            migrationBuilder.InsertData(
                table: "ScoringEventTypes",
                columns: new[] { "Id", "EventDescription" },
                values: new object[,]
                {
                    { 1, "ידני" },
                    { 2, "פעולה" },
                    { 3, "חישוב" }
                });

            migrationBuilder.InsertData(
                table: "ScroingOperations",
                columns: new[] { "Id", "ActionId", "Count", "EventTypeId", "FromDate", "TillDate" },
                values: new object[,]
                {
                    { 1, 1, 20, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 2, 2, 7, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 3, 3, 23, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 4, 4, 8, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 5, 5, 12, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 6, 6, 15, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 7, 7, 7, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 8, 8, 3, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 9, 9, 3, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 10, 10, 3, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 11, 11, 3, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 12, 12, 12, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 13, 13, 12, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null }
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
                name: "IX_ScroingOperations_ActionId",
                table: "ScroingOperations",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScroingOperations_EventTypeId",
                table: "ScroingOperations",
                column: "EventTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessScorings");

            migrationBuilder.DropTable(
                name: "ScroingOperations");

            migrationBuilder.DropTable(
                name: "ScoringActions");

            migrationBuilder.DropTable(
                name: "ScoringEventTypes");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Buisness");

            migrationBuilder.DropColumn(
                name: "ScoreStatus",
                table: "Buisness");
        }
    }
}
