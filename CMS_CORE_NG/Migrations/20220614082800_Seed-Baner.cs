using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class SeedBaner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DefaultLink", "DefaultPicGuid", "Title" },
                values: new object[] { "/advertisments-catalog", new Guid("5f726d18-a68b-497b-bec0-940e2dac3be1"), "דף הבית מעל המודעות" });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DefaultLink", "DefaultPicGuid", "PageID", "PageName", "Price", "Title", "Width" },
                values: new object[] { "/advertisments-catalog", new Guid("a89c20ea-d971-4e00-a7db-be9b90eeb1da"), 1, "דף הבית", 200f, "דף הבית מתחת מודעות", 100 });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DefaultLink", "DefaultPicGuid", "Height", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), 180, 2, "דף חיפוש עסקים", 200f, 5, "מעל רשימת העסקים", 100 });

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "1 פרסומת בדף הבית");

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "פרסומת בדף הבית 2");

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "פרסומת בדף חיפוש");

            migrationBuilder.InsertData(
                table: "CatalogServices",
                columns: new[] { "Id", "Description", "Makat", "ServiceTypeId" },
                values: new object[,]
                {
                    { 4, "פרסומת בדף עסק", 444, 1 },
                    { 5, "פסומת בלוח מודעות", 555, 1 },
                    { 6, "B2B", 12345, 2 }
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 4, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), 180, 444, 3, "דף עסק", 200f, 5, "בראש דף העסק", 100 });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 5, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), 80, 555, 4, "לוח מודעות", 250f, 15, "פרסומת בדף מודעות", 25 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DefaultLink", "DefaultPicGuid", "Title" },
                values: new object[] { null, null, "פסומת בדף הבית" });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DefaultLink", "DefaultPicGuid", "PageID", "PageName", "Price", "Title", "Width" },
                values: new object[] { null, null, 4, "דף עסק", 100f, "פסומת בדף עסק", 180 });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DefaultLink", "DefaultPicGuid", "Height", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { null, null, 80, 6, "לוח מודעות", 250f, 15, "פסומת בדף מודעות", 80 });

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "פסומת בדף הבית");

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "פסומת בדף חיפוש עסקים");

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "פסומת בדף מודעות");

            migrationBuilder.InsertData(
                table: "CatalogServices",
                columns: new[] { "Id", "Description", "Makat", "ServiceTypeId" },
                values: new object[] { 4, "B2B", 12345, 2 });
        }
    }
}
