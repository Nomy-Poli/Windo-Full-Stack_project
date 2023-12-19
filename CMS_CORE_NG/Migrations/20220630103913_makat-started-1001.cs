using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class makatstarted1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.InsertData(
                table: "CatalogServices",
                columns: new[] { "Id", "Description", "Makat", "ServiceTypeId" },
                values: new object[,]
                {
                    { 7, "באנר תחתון במודעות", 1007, 1 },
                    { 6, "באנר עליון במודעות", 1006, 1 },
                    { 5, "באנר עליון בדף העסק", 1005, 1 },
                    { 4, "באנר תחתון בדף עסקים", 1004, 1 },
                    { 3, "פרסומת בדף חיפוש עסקים", 1003, 1 },
                    { 2, "פרסומת בדף הבית 2", 1002, 1 },
                    { 1, "1 פרסומת בדף הבית", 1001, 1 },
                    { 8, "B2B", 1008, 2 }
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "ExamplePicGuid", "FormatPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[,]
                {
                    { 7, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 80, 1007, 4, "לוח מודעות", 250f, 15, "באנר תחתון במודעות", 25 },
                    { 6, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 80, 1006, 4, "לוח מודעות", 250f, 15, "באנר עליון במודעות", 25 },
                    { 5, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 1005, 3, "דף עסק", 200f, 5, "בראש דף העסק", 100 },
                    { 4, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 1004, 2, "דף חיפוש עסקים", 200f, 5, "באנר תחתון בדף חיפוש עסקים", 100 },
                    { 3, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 1003, 2, "דף חיפוש עסקים", 200f, 5, "באנר עליון בדף חיפוש עסקים", 100 },
                    { 2, null, "/advertisments-catalog", new Guid("a89c20ea-d971-4e00-a7db-be9b90eeb1da"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 1002, 1, "דף הבית", 200f, 5, "באנר נוסף בדף הבית", 100 },
                    { 1, null, "/advertisments-catalog", new Guid("5f726d18-a68b-497b-bec0-940e2dac3be1"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 300, 1001, 1, "דף הבית", 200f, 10, "באנר מרכזי בדף הבית", 100 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.InsertData(
                table: "CatalogServices",
                columns: new[] { "Id", "Description", "Makat", "ServiceTypeId" },
                values: new object[,]
                {
                    { 7, "באנר תחתון במודעות", 777, 1 },
                    { 6, "באנר עליון במודעות", 666, 1 },
                    { 5, "באנר עליון בדף העסק", 555, 1 },
                    { 4, "באנר תחתון בדף עסקים", 444, 1 },
                    { 3, "פרסומת בדף חיפוש עסקים", 333, 1 },
                    { 2, "פרסומת בדף הבית 2", 222, 1 },
                    { 1, "1 פרסומת בדף הבית", 111, 1 },
                    { 8, "B2B", 12345, 2 }
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "ExamplePicGuid", "FormatPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[,]
                {
                    { 7, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 80, 777, 4, "לוח מודעות", 250f, 15, "באנר תחתון במודעות", 25 },
                    { 6, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 80, 666, 4, "לוח מודעות", 250f, 15, "באנר עליון במודעות", 25 },
                    { 5, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 555, 3, "דף עסק", 200f, 5, "בראש דף העסק", 100 },
                    { 4, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 444, 2, "דף חיפוש עסקים", 200f, 5, "באנר תחתון בדף חיפוש עסקים", 100 },
                    { 3, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 333, 2, "דף חיפוש עסקים", 200f, 5, "באנר עליון בדף חיפוש עסקים", 100 },
                    { 2, null, "/advertisments-catalog", new Guid("a89c20ea-d971-4e00-a7db-be9b90eeb1da"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 222, 1, "דף הבית", 200f, 5, "באנר נוסף בדף הבית", 100 },
                    { 1, null, "/advertisments-catalog", new Guid("5f726d18-a68b-497b-bec0-940e2dac3be1"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 300, 111, 1, "דף הבית", 200f, 10, "באנר מרכזי בדף הבית", 100 }
                });
        }
    }
}
