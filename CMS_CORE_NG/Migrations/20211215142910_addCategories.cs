using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class addCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 23,
                column: "name",
                value: "פאות");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 29, "אחר" },
                    { 28, "מקצועות במה" },
                    { 24, "רפואה משלימה" },
                    { 26, "הריון ולידה" },
                    { 25, "טיפולים שונים" },
                    { 27, "אבחונים" }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 140,
                column: "name",
                value: "סירוק פאות");

            migrationBuilder.InsertData(
                table: "SubCategory",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 161, "אנימציה" },
                    { 162, "זמרת" },
                    { 163, "שחקנית" },
                    { 164, "נגנית" },
                    { 165, "רקדנית" },
                    { 166, "מפעילת תוכניות" },
                    { 167, "קוסמת" },
                    { 168, "מתנות" },
                    { 170, "תכשיטים" },
                    { 171, "אקססוריז" },
                    { 172, "מוצרי חשמל" },
                    { 173, "תוספי תזונה" },
                    { 174, "אחר" },
                    { 175, "התעמלות" },
                    { 176, "דיקור סיני" },
                    { 169, "משחקים" },
                    { 160, "משכנתאות ומימון" },
                    { 158, "דולה" },
                    { 157, "הכנה ללידה" },
                    { 141, "ייצור פאות" },
                    { 142, "תפירת פאות" },
                    { 143, "תיקונים" },
                    { 144, "צביעה" },
                    { 145, "הומאופטיה" },
                    { 146, "נטורופתיה" },
                    { 147, "אירידיולוגיה" },
                    { 148, "נרות הופי" },
                    { 149, "אוסטיאופתיה" },
                    { 150, "צמחי באך" },
                    { 151, "הידרותרפיה" },
                    { 152, "דיאטנית" },
                    { 153, "גרפולוגיה" },
                    { 154, "מורפולוגיה" },
                    { 155, "כירולוגיה" },
                    { 159, "מיילדת" },
                    { 156, "אבחון דידקטי" }
                });

            migrationBuilder.InsertData(
                table: "CategorySubCategory",
                columns: new[] { "Id", "categoryId", "subCategoryId" },
                values: new object[,]
                {
                    //{ 141, 23, 141 },
                    //{ 176, 1, 174 },
                    //{ 177, 2, 174 },
                    //{ 178, 3, 174 },
                    //{ 179, 5, 174 },

                    //{ 180, 6, 174 },
                    //{ 181, 7, 174 },
                    //{ 183, 10, 174 },
                    //{ 184, 11, 174 },
                    //{ 185, 12, 174 },
                    //{ 186, 13, 174 },
                    //{ 187, 14, 174 },
                    //{ 188, 15, 174 },
                    //{ 189, 16, 174 },

                    //{ 190, 17, 174 },
                    //{ 191, 18, 174 },
                    //{ 192, 19, 174 },
                    //{ 193, 20, 174 },
                    //{ 194, 21, 174 },
                    //{ 195, 22, 174 },
                    //{ 196, 23, 174 },
                    //{ 197, 24, 174 },
                    //{ 198, 25, 174 },
                    //{ 199, 26, 174 },

                    //{ 200, 27, 174 },
                    //{ 201, 28, 174 },
                    //{ 202, 29, 174 },
                    //{ 203, 9, 174 },
                    //{ 204, 8, 174 },
                    //{ 205, 4, 174 },

                    //{ 175, 16, 173 },
                    //{ 182, 9, 175 },
                    //{ 174, 16, 172 },
                    //{ 172, 16, 170 },

                    //{ 142, 23, 142 },
                    //{ 143, 23, 143 },
                    //{ 144, 23, 144 },
                    //{ 145, 24, 145 },
                    //{ 146, 24, 146 },
                    //{ 148, 24, 147 },
                    //{ 149, 24, 148 },

                    //{ 150, 24, 149 },
                    //{ 151, 24, 150 },
                    //{ 152, 25, 150 },
                    //{ 153, 25, 151 },
                    //{ 154, 25, 152 },
                    //{ 155, 27, 153 },
                    //{ 156, 27, 154 },
                    //{ 157, 27, 155 },
                    //{ 158, 27, 156 },
                    //{ 159, 26, 157 },

                    //{ 160, 26, 158 },
                    //{ 161, 26, 159 },
                    //{ 168, 18, 160 },
                    //{ 169, 7, 161 },
                    //{ 162, 28, 162 },
                    //{ 163, 28, 163 },
                    //{ 164, 28, 164 },
                    //{ 165, 28, 165 },
                    //{ 166, 28, 166 },
                    //{ 167, 28, 167 },

                    //{ 170, 16, 168 },
                    //{ 171, 16, 169 },
                    //{ 173, 16, 171 },
                    //{ 147, 24, 176 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "CategorySubCategory",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 23,
                column: "name",
                value: "אחר");

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 140,
                column: "name",
                value: "אחר");
        }
    }
}
