using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class MessagelatedBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentMessagesId = table.Column<Guid>(nullable: true),
                    BusinessIdFrom = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    MessageText = table.Column<string>(nullable: true),
                    CollaborationType = table.Column<int>(nullable: true),
                    MessageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Buisness_BusinessIdFrom",
                        column: x => x.BusinessIdFrom,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessagesTo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<Guid>(nullable: false),
                    BusinessIdTo = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: true),
                    IsNew = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesTo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessagesTo_Buisness_BusinessIdTo",
                        column: x => x.BusinessIdTo,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessagesTo_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CategorySubCategory",
                columns: new[] { "Id", "categoryId", "subCategoryId" },
                values: new object[] { 229, 29, 174 });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BusinessIdFrom",
                table: "Messages",
                column: "BusinessIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageId",
                table: "Messages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagesTo_BusinessIdTo",
                table: "MessagesTo",
                column: "BusinessIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_MessagesTo_MessageId",
                table: "MessagesTo",
                column: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagesTo");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 229);
        }
    }
}
