using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class Notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    BusinessId = table.Column<int>(nullable: false),
                    CategorySubCategoryId = table.Column<int>(nullable: true),
                    Labels = table.Column<string>(nullable: true),
                    CreatetionDate = table.Column<DateTime>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    DeletionDate = table.Column<DateTime>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    ChangedStatus = table.Column<int>(nullable: true),
                    NumberOfViews = table.Column<int>(nullable: true),
                    IsBold = table.Column<bool>(nullable: true),
                    IsPayNote = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Buisness_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_CategorySubCategory_CategorySubCategoryId",
                        column: x => x.CategorySubCategoryId,
                        principalTable: "CategorySubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotesBoards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<int>(nullable: false),
                    BoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesBoards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotesBoards_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotesBoards_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplayNoteMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<int>(nullable: false),
                    MessageId = table.Column<Guid>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplayNoteMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplayNoteMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ReplayNoteMessages_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Color", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "הצעות ובקשות לשיתופי פעולה ספציפיים ועדכניים , שקורים ממש עכשיו.", "דרושים" },
                    { 2, null, "עצה ועזרה הדדית.", "עצות" }
                });

          

            migrationBuilder.CreateIndex(
                name: "IX_Notes_BusinessId",
                table: "Notes",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CategorySubCategoryId",
                table: "Notes",
                column: "CategorySubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesBoards_BoardId",
                table: "NotesBoards",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesBoards_NoteId",
                table: "NotesBoards",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplayNoteMessages_MessageId",
                table: "ReplayNoteMessages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplayNoteMessages_NoteId",
                table: "ReplayNoteMessages",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesBoards");

            migrationBuilder.DropTable(
                name: "ReplayNoteMessages");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Notes");

        
        }
    }
}
