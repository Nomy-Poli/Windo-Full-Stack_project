using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class AddCategories : Migration
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
                    { 32, "אחר" },
                    { 31, "קורסים דיגיטליים" },
                    { 30, "בריאות" },
                    { 29, "תיירות ונופש" },
                    { 24, "רפואה משלימה" },
                    { 27, "אבחונים" },
                    { 26, "הריון ולידה" },
                    { 25, "טיפולים שונים" },
                    { 28, "מקצועות במה" }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 101,
                column: "name",
                value: "קייטרינג חלבי");

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
                    { 182, "הרצאות וסדנאות על בריאות" },
                    { 181, "הסעות" },
                    { 180, "טיולים" },
                    { 179, "הסעדה ומזון" },
                    { 178, "אטרקציות" },
                    { 175, "התעמלות" },
                    { 176, "דיקור סיני" },
                    { 174, "אחר" },
                    { 173, "תוספי תזונה" },
                    { 172, "מוצרי חשמל" },
                    { 177, "אירוח" },
                    { 183, "מכירת תוספי תזונה" },
                    { 185, "התעמלות" },
                    { 186, "לפני ואחרי לידה" },
                    { 187, "עיצוב" },
                    { 188, "עסקי" },
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
                    { 171, "אקססוריז" },
                    { 169, "משחקים" },
                    { 168, "מתנות" },
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
                    { 170, "תכשיטים" },
                    { 153, "גרפולוגיה" },
                    { 155, "כירולוגיה" },
                    { 156, "אבחון דידקטי" },
                    { 157, "הכנה ללידה" },
                    { 158, "דולה" },
                    { 159, "מיילדת" },
                    { 160, "משכנתאות ומימון" },
                    { 161, "אנימציה" },
                    { 162, "זמרת" },
                    { 163, "שחקנית" },
                    { 164, "נגנית" },
                    { 165, "רקדנית" },
                    { 167, "קוסמת" },
                    { 154, "מורפולוגיה" },
                    { 166, "מפעילת תוכניות" }
                });

            migrationBuilder.InsertData(
                table: "CategorySubCategory",
                columns: new[] { "Id", "categoryId", "subCategoryId" },
                values: new object[,]
                {
                    { 141, 23, 141 },
                    { 226, 30, 174 },
                    { 211, 29, 174 },
                    { 205, 14, 174 },
                    { 204, 8, 174 },
                    { 203, 9, 174 },
                    { 202, 29, 174 },
                    { 201, 28, 174 },
                    { 200, 27, 174 },
                    { 227, 31, 174 },
                    { 199, 26, 174 },
                    { 197, 24, 174 },
                    { 196, 23, 174 },
                    { 195, 22, 174 },
                    { 194, 21, 174 },
                    { 193, 20, 174 },
                    { 192, 19, 174 },
                    { 191, 18, 174 },
                    { 190, 17, 174 },
                    { 198, 25, 174 },
                    { 189, 16, 174 },
                    { 228, 4, 174 },
                    { 147, 24, 176 },
                    { 224, 31, 194 },
                    { 223, 31, 193 },
                    { 222, 31, 192 },
                    { 221, 31, 191 },
                    { 220, 31, 190 },
                    { 219, 31, 189 },
                    { 218, 31, 188 },
                    { 217, 31, 187 },
                    { 182, 9, 175 },
                    { 216, 30, 186 },
                    { 214, 30, 184 },
                    { 213, 30, 183 },
                    { 212, 30, 182 },
                    { 206, 29, 181 },
                    { 210, 29, 180 },
                    { 209, 29, 179 },
                    { 208, 29, 178 },
                    { 207, 29, 177 },
                    { 215, 30, 185 },
                    { 188, 15, 174 },
                    { 186, 13, 174 },
                    { 185, 12, 174 },
                    { 160, 26, 158 },
                    { 159, 26, 157 },
                    { 158, 27, 156 },
                    { 157, 27, 155 },
                    { 156, 27, 154 },
                    { 155, 27, 153 },
                    { 154, 25, 152 },
                    { 153, 25, 151 },
                    { 161, 26, 159 },
                    { 152, 25, 150 },
                    { 150, 24, 149 },
                    { 149, 24, 148 },
                    { 148, 24, 147 },
                    { 146, 24, 146 },
                    { 145, 24, 145 },
                    { 144, 23, 144 },
                    { 143, 23, 143 },
                    { 142, 23, 142 },
                    { 151, 24, 150 },
                    { 168, 18, 160 },
                    { 169, 7, 161 },
                    { 162, 28, 162 },
                    { 184, 11, 174 },
                    { 183, 10, 174 },
                    { 181, 7, 174 },
                    { 180, 6, 174 },
                    { 179, 5, 174 },
                    { 178, 3, 174 },
                    { 177, 2, 174 },
                    { 176, 1, 174 },
                    { 175, 16, 173 },
                    { 174, 16, 172 },
                    { 173, 16, 171 },
                    { 172, 16, 170 },
                    { 171, 16, 169 },
                    { 170, 16, 168 },
                    { 167, 28, 167 },
                    { 166, 28, 166 },
                    { 165, 28, 165 },
                    { 164, 28, 164 },
                    { 163, 28, 163 },
                    { 187, 14, 195 },
                    { 225, 31, 195 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 197);

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
                table: "Category",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 31);

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
                keyValue: 23,
                column: "name",
                value: "אחר");

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 101,
                column: "name",
                value: "קייטנרינג חלבי");

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 140,
                column: "name",
                value: "אחר");
        }
    }
}
