using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class addmanualoperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ScroingOperations",
                columns: new[] { "Id", "ActionId", "Count", "EventTypeId", "FromDate", "TillDate" },
                values: new object[,]
                {
                    { 14, 1, 20, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 15, 2, 7, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 16, 3, 23, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 17, 4, 8, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 18, 5, 12, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 19, 6, 15, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 20, 7, 7, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 21, 8, 3, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 22, 9, 1, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 23, 10, 1, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 24, 11, 1, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 25, 12, 12, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null },
                    { 26, 13, 12, 1, new DateTime(2022, 8, 17, 23, 28, 56, 782, DateTimeKind.Utc), null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ScroingOperations",
                keyColumn: "Id",
                keyValue: 26);
        }
    }
}
