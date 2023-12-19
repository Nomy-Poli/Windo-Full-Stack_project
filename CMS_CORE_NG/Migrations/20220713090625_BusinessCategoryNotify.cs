using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class BusinessCategoryNotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WantedGetDailyNotification",
                table: "Buisness",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WantedGetHelpNotification",
                table: "Buisness",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessCategoriesNotify",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(nullable: false),
                    categoryId = table.Column<int>(nullable: false),
                    IfNotify = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCategoriesNotify", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCategoriesNotify_Buisness_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessCategoriesNotify_Category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCategoriesNotify_BusinessId",
                table: "BusinessCategoriesNotify",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCategoriesNotify_categoryId",
                table: "BusinessCategoriesNotify",
                column: "categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCategoriesNotify");

            migrationBuilder.DropColumn(
                name: "WantedGetDailyNotification",
                table: "Buisness");

            migrationBuilder.DropColumn(
                name: "WantedGetHelpNotification",
                table: "Buisness");
        }
    }
}
