using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class AddNewMakatimToBaner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "ExamplePicGuid", "FormatPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 8, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 80, 1008, 5, "דף עצמאית", 250f, 15, "באנר בדף עצמאית", 25 });

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ServiceTypeId" },
                values: new object[] { "פרסומת בדף עצמאית", 1 });

            migrationBuilder.InsertData(
                table: "CatalogServices",
                columns: new[] { "Id", "Description", "Makat", "ServiceTypeId" },
                values: new object[,]
                {
                    { 9, "פרסומת בדף שכירה ", 1009, 1 },
                    { 10, "פרסומת בדף ההאב", 1010, 1 }
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "ExamplePicGuid", "FormatPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 9, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 80, 1009, 6, "דף שכירה", 250f, 15, "באנר בדף שכירה", 25 });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "ExamplePicGuid", "FormatPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 10, null, "/advertisments-catalog", new Guid("32af284f-ef35-4255-b363-2b4248664383"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), null, 180, 1010, 7, "דף ההאב", 250f, 15, "באנר בדף ההאב ", 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "CatalogServices",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ServiceTypeId" },
                values: new object[] { "B2B", 2 });
        }
    }
}
