using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class categoriesMessageNotes : Migration
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        name: "FK_ReplayNoteMessages_Buisness_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 29,
                column: "name",
                value: "תיירות ונופש");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    //{ 25, "טיפולים שונים" },
                    //{ 26, "הריון ולידה" },
                    //{ 27, "אבחונים" },
                    //{ 28, "מקצועות במה" },
                    //{ 29, "תיירות ונופש" },
                    { 30, "בריאות" },
                    { 31, "קורסים דיגיטליים" },
                    { 32, "אחר" },
                    //////////{ 24, "רפואה משלימה" }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 101,
                column: "name",
                value: "קייטרינג חלבי");

            //migrationBuilder.UpdateData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 140,
            //    column: "name",
            //    value: "סירוק פאות");

            migrationBuilder.InsertData(
                table: "SubCategory",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 181, "הסעות" },
                    { 180, "טיולים" },
                    { 179, "הסעדה ומזון" },
                    { 178, "אטרקציות" },
                    //{ 174, "אחר" },
                    //{ 176, "דיקור סיני" },
                    //{ 175, "התעמלות" },
                    { 182, "הרצאות וסדנאות על בריאות" },
                    //{ 173, "תוספי תזונה" },
                    { 177, "אירוח" },
                    { 183, "מכירת תוספי תזונה" },
                    { 188, "עסקי" },
                    { 185, "התעמלות" },
                    { 186, "לפני ואחרי לידה" },
                    { 187, "עיצוב" },
                    //{ 172, "מוצרי חשמל" },
                    { 189, "פיננסי" },
                    { 190, "דיגיטל" },
                    { 191, "שיווק ומכירות" },
                    { 192, "קולינריה" },
                    { 193, "בריאות" },
                    { 194, "זוגיות והורות" },
                    { 195, "לימוד מקצוע" },
                    { 196, "עיצובי פירות וירקות" },
                    { 197, "חבילות נופש" },
                    { 184, "ייעוץ אישי" },
                    //{ 171, "אקססוריז" },
                    //{ 166, "מפעילת תוכניות" },
                    //{ 169, "משחקים" },
                    //{ 141, "ייצור פאות" },
                    //{ 142, "תפירת פאות" },
                    //{ 143, "תיקונים" },
                    //{ 144, "צביעה" },
                    //{ 145, "הומאופטיה" },
                    //{ 146, "נטורופתיה" },
                    //{ 147, "אירידיולוגיה" },
                    //{ 148, "נרות הופי" },
                    //{ 149, "אוסטיאופתיה" },
                    //{ 150, "צמחי באך" },
                    //{ 151, "הידרותרפיה" },
                    //{ 152, "דיאטנית" },
                    //{ 170, "תכשיטים" },
                    //{ 153, "גרפולוגיה" },
                    //{ 155, "כירולוגיה" },
                    //{ 156, "אבחון דידקטי" },
                    //{ 157, "הכנה ללידה" },
                    //{ 158, "דולה" },
                    //{ 159, "מיילדת" },
                    //{ 160, "משכנתאות ומימון" },
                    //{ 161, "אנימציה" },
                    //{ 162, "זמרת" },
                    //{ 163, "שחקנית" },
                    //{ 164, "נגנית" },
                    //{ 167, "קוסמת" },
                    //{ 168, "מתנות" },
                    //{ 154, "מורפולוגיה" },
                    //{ 165, "רקדנית" }
                });

            migrationBuilder.InsertData(
                table: "CategorySubCategory",
                columns: new[] { "Id", "categoryId", "subCategoryId" },
                values: new object[,]
                {
                    //{ 141, 23, 141 },
                    { 226, 30, 174 },
                    { 211, 29, 174 },
                    //{ 205, 14, 174 },
                    //{ 204, 8, 174 },
                    //{ 203, 9, 174 },
                    //{ 202, 29, 174 },
                    //{ 201, 28, 174 },
                    //{ 200, 27, 174 },
                    { 227, 31, 174 },
                    //{ 199, 26, 174 },
                    //{ 197, 24, 174 },
                    //{ 196, 23, 174 },
                    //{ 195, 22, 174 },
                    //{ 194, 21, 174 },
                    //{ 193, 20, 174 },
                    //{ 192, 19, 174 },
                    //{ 191, 18, 174 },
                    //{ 190, 17, 174 },
                    //{ 198, 25, 174 },
                    { 228, 4, 174 },
                    { 229, 29, 174 },
                    //{ 182, 9, 175 },
                    { 224, 31, 194 },
                    { 223, 31, 193 },
                    { 222, 31, 192 },
                    { 221, 31, 191 },
                    { 220, 31, 190 },
                    { 219, 31, 189 },
                    { 218, 31, 188 },
                    { 217, 31, 187 },
                    { 216, 30, 186 },
                    { 215, 30, 185 },
                    { 214, 30, 184 },
                    { 213, 30, 183 },
                    { 212, 30, 182 },
                    { 206, 29, 181 },
                    { 210, 29, 180 },
                    { 209, 29, 179 },
                    { 208, 29, 178 },
                    { 207, 29, 177 },
                    //{ 147, 24, 176 },
                    //{ 189, 16, 174 },
                    //{ 187, 14, 195 },
                    //{ 188, 15, 174 },
                    //{ 185, 12, 174 },
                    //{ 160, 26, 158 },
                    //{ 159, 26, 157 },
                    //{ 158, 27, 156 },
                    //{ 157, 27, 155 },
                    //{ 156, 27, 154 },
                    //{ 155, 27, 153 },
                    //{ 154, 25, 152 },
                    //{ 153, 25, 151 },
                    //{ 161, 26, 159 },
                    //{ 152, 25, 150 },
                    //{ 150, 24, 149 },
                    //{ 149, 24, 148 },
                    //{ 148, 24, 147 },
                    //{ 146, 24, 146 },
                    ////{ 145, 24, 145 },
                    //{ 144, 23, 144 },
                    //{ 143, 23, 143 },
                    //{ 142, 23, 142 },
                    //{ 151, 24, 150 },
                    //{ 168, 18, 160 },
                    //{ 169, 7, 161 },
                    //{ 162, 28, 162 },
                    //{ 184, 11, 174 },
                    //{ 183, 10, 174 },
                    //{ 181, 7, 174 },
                    //{ 180, 6, 174 },
                    //{ 179, 5, 174 },
                    //{ 178, 3, 174 },
                    //{ 177, 2, 174 },
                    //{ 176, 1, 174 },
                    //{ 175, 16, 173 },
                    //{ 174, 16, 172 },
                    //{ 173, 16, 171 },
                    //{ 172, 16, 170 },
                    //{ 171, 16, 169 },
                    //{ 170, 16, 168 },
                    //{ 167, 28, 167 },
                    //{ 166, 28, 166 },
                    //{ 165, 28, 165 },
                    //{ 164, 28, 164 },
                    //{ 163, 28, 163 },
                    //{ 186, 13, 174 },
                    { 225, 31, 195 }
                });

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
                name: "IX_ReplayNoteMessages_BusinessId",
                table: "ReplayNoteMessages",
                column: "BusinessId");

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
                name: "MessagesTo");

            migrationBuilder.DropTable(
                name: "NotesBoards");

            migrationBuilder.DropTable(
                name: "ReplayNoteMessages");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 169);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 170);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 171);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 172);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 173);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 174);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 175);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 176);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 177);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 178);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 179);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 180);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 181);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 182);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 183);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 184);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 185);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 186);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 187);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 188);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 189);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 190);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 191);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 192);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 193);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 194);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 195);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 196);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 197);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 198);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 199);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 200);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 201);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 202);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 203);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 204);

            //migrationBuilder.DeleteData(
            //    table: "CategorySubCategory",
            //    keyColumn: "Id",
            //    keyValue: 205);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 197);

            //migrationBuilder.DeleteData(
            //    table: "Category",
            //    keyColumn: "Id",
            //    keyValue: 24);

            //migrationBuilder.DeleteData(
            //    table: "Category",
            //    keyColumn: "Id",
            //    keyValue: 25);

            //migrationBuilder.DeleteData(
            //    table: "Category",
            //    keyColumn: "Id",
            //    keyValue: 26);

            //migrationBuilder.DeleteData(
            //    table: "Category",
            //    keyColumn: "Id",
            //    keyValue: 27);

            //migrationBuilder.DeleteData(
            //    table: "Category",
            //    keyColumn: "Id",
            //    keyValue: 28);

            //migrationBuilder.DeleteData(
            //    table: "Category",
            //    keyColumn: "Id",
            //    keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 31);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 141);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 142);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 143);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 144);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 145);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 146);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 147);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 148);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 149);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 150);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 151);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 152);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 153);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 154);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 155);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 156);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 157);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 158);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 159);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 160);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 161);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 162);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 163);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 164);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 165);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 166);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 167);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 168);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 169);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 170);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 171);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 172);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 173);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 174);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 175);

            //migrationBuilder.DeleteData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 176);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 29,
                column: "name",
                value: "אחר");

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 101,
                column: "name",
                value: "קייטנרינג חלבי");

            //migrationBuilder.UpdateData(
            //    table: "SubCategory",
            //    keyColumn: "Id",
            //    keyValue: 140,
            //    column: "name",
            //    value: "אחר");
        }
    }
}
