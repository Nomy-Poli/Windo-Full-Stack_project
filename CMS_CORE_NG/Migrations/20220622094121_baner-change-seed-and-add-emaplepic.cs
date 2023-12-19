using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class banerchangeseedandaddemaplepic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<Guid>(
                name: "ExamplePicGuid",
                table: "Banners",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExamplePicGuid", "Title" },
                values: new object[] { new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "באנר מרכזי בדף הבית" });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExamplePicGuid", "Title" },
                values: new object[] { new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "באנר נוסף בדף הבית" });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExamplePicGuid", "Title" },
                values: new object[] { new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "באנר עליון בדף חיפוש עסקים" });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExamplePicGuid", "PageID", "PageName", "Title" },
                values: new object[] { new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 2, "דף חיפוש עסקים", "באנר תחתון בדף חיפוש עסקים" });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ExamplePicGuid", "Height", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 180, 3, "דף עסק", 200f, 5, "בראש דף העסק", 100 });

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "פרסומת בדף חיפוש עסקים");

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "באנר תחתון בדף עסקים");

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "באנר עליון בדף העסק");

            migrationBuilder.InsertData(
                table: "CatalogServices",
                columns: new[] { "Id", "Description", "Makat", "ServiceTypeId" },
                values: new object[,]
                {
                    { 6, "באנר עליון במודעות", 666, 1 },
                    { 7, "באנר תחתון במודעות", 777, 1 },
                    { 8, "B2B", 12345, 2 }
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "ExamplePicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 6, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 80, 666, 4, "לוח מודעות", 250f, 15, "באנר עליון במודעות", 25 });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "ExamplePicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 7, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 80, 777, 4, "לוח מודעות", 250f, 15, "באנר תחתון במודעות", 25 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "ExamplePicGuid",
                table: "Banners");

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "דף הבית מעל המודעות");

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "דף הבית מתחת מודעות");

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "מעל רשימת העסקים");

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PageID", "PageName", "Title" },
                values: new object[] { 3, "דף עסק", "בראש דף העסק" });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Height", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 80, 4, "לוח מודעות", 250f, 15, "פרסומת בדף מודעות", 25 });

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "פרסומת בדף חיפוש");

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "פרסומת בדף עסק");

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "פסומת בלוח מודעות");

            migrationBuilder.InsertData(
                table: "CatalogServices",
                columns: new[] { "Id", "Description", "Makat", "ServiceTypeId" },
                values: new object[] { 6, "B2B", 12345, 2 });
        }
    }
}
