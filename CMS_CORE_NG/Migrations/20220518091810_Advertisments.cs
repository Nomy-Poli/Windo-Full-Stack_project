using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class Advertisments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequsetsOrderService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Makat = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 10, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 10, nullable: true),
                    Text = table.Column<string>(maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ServiceDate = table.Column<DateTime>(nullable: true),
                    ReturningCustomer = table.Column<bool>(nullable: true),
                    LinkForSite = table.Column<string>(nullable: true),
                    RequestStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequsetsOrderService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 80, nullable: true),
                    ClientTypeId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 10, nullable: true),
                    UserEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_ClientTypes_ClientTypeId",
                        column: x => x.ClientTypeId,
                        principalTable: "ClientTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Makat = table.Column<int>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogServices", x => x.Id);
                    table.UniqueConstraint("AK_CatalogServices_Makat", x => x.Makat);
                    table.ForeignKey(
                        name: "FK_CatalogServices_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    StatusOrderId = table.Column<int>(nullable: false),
                    Makat = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Price = table.Column<float>(nullable: true),
                    CatalogServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderServices_CatalogServices_CatalogServiceId",
                        column: x => x.CatalogServiceId,
                        principalTable: "CatalogServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderServices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderServices_OrderStatuses_StatusOrderId",
                        column: x => x.StatusOrderId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertismentServiceOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderServiceId = table.Column<int>(nullable: false),
                    Makat = table.Column<int>(nullable: false),
                    adFromDate = table.Column<DateTime>(nullable: true),
                    adTillDate = table.Column<DateTime>(nullable: true),
                    PicGuid = table.Column<Guid>(nullable: true),
                    LinkToSite = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertismentServiceOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertismentServiceOrders_OrderServices_OrderServiceId",
                        column: x => x.OrderServiceId,
                        principalTable: "OrderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Makat = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    PageName = table.Column<string>(nullable: true),
                    PageID = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    PriceInPoints = table.Column<int>(nullable: true),
                    Price = table.Column<float>(nullable: true),
                    DefaultPicGuid = table.Column<Guid>(nullable: true),
                    DefaultLink = table.Column<string>(nullable: true),
                    AdvertismentServiceOrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banners_AdvertismentServiceOrders_AdvertismentServiceOrderId",
                        column: x => x.AdvertismentServiceOrderId,
                        principalTable: "AdvertismentServiceOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Banners_CatalogServices_Makat",
                        column: x => x.Makat,
                        principalTable: "CatalogServices",
                        principalColumn: "Makat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClientTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "מנוי" },
                    { 2, "מפרסם" },
                    { 3, "בזק" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "מחכה למנהל" },
                    { 2, "אושר לא שולם" },
                    { 3, "אושר ושולם" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "הצבת פרסומות על דפי האתר", "פרסום" },
                    { 2, "", "B2B" }
                });

            migrationBuilder.InsertData(
                table: "CatalogServices",
                columns: new[] { "Id", "Description", "Makat", "ServiceTypeId" },
                values: new object[,]
                {
                    { 1, "פסומת בדף הבית", 111, 1 },
                    { 2, "פסומת בדף חיפוש עסקים", 222, 1 },
                    { 3, "פסומת בדף מודעות", 333, 1 },
                    { 4, "B2B", 12345, 2 }
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 1, null, null, null, 300, 111, 1, "דף הבית", 200f, 10, "פסומת בדף הבית", 100 });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 2, null, null, null, 180, 222, 4, "דף עסק", 100f, 5, "פסומת בדף עסק", 180 });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "AdvertismentServiceOrderId", "DefaultLink", "DefaultPicGuid", "Height", "Makat", "PageID", "PageName", "Price", "PriceInPoints", "Title", "Width" },
                values: new object[] { 3, null, null, null, 80, 333, 6, "לוח מודעות", 250f, 15, "פסומת בדף מודעות", 80 });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertismentServiceOrders_OrderServiceId",
                table: "AdvertismentServiceOrders",
                column: "OrderServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banners_AdvertismentServiceOrderId",
                table: "Banners",
                column: "AdvertismentServiceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_Makat",
                table: "Banners",
                column: "Makat");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogServices_ServiceTypeId",
                table: "CatalogServices",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientTypeId",
                table: "Clients",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_CatalogServiceId",
                table: "OrderServices",
                column: "CatalogServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_ClientId",
                table: "OrderServices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_StatusOrderId",
                table: "OrderServices",
                column: "StatusOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "RequsetsOrderService");

            migrationBuilder.DropTable(
                name: "AdvertismentServiceOrders");

            migrationBuilder.DropTable(
                name: "OrderServices");

            migrationBuilder.DropTable(
                name: "CatalogServices");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "ClientTypes");
        }
    }
}
