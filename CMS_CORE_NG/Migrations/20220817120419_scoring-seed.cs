using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class scoringseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDescribe",
                table: "ScoringActions");

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId",
                table: "ScoringActions",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "Id", "ActionId", "Count", "EventTypeId", "FromDate", "ScoringActionId", "TillDate" },
                values: new object[,]
                {
                    { 1, 1, 20, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 2, 2, 7, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 3, 3, 23, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 4, 4, 8, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 5, 5, 12, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 6, 6, 15, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 7, 7, 7, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 8, 8, 3, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 9, 9, 3, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 10, 10, 3, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 11, 11, 3, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 12, 12, 12, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null },
                    { 13, 13, 12, 2, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ScoringActions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ScoringEventTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScoringEventTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ScoringEventTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "EventTypeId",
                table: "ScoringActions");

            migrationBuilder.AddColumn<string>(
                name: "ActionDescribe",
                table: "ScoringActions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
