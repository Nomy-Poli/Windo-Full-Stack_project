using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class addCustomerResponsesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerResponses",
                table: "CaseStudies");

            migrationBuilder.AddColumn<bool>(
                name: "IfMainPicture",
                table: "CaseStudyPictures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerResponses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(nullable: true),
                    MinimalDescription = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    CaseStudyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerResponses_CaseStudies_CaseStudyId",
                        column: x => x.CaseStudyId,
                        principalTable: "CaseStudies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Area",
                keyColumn: "Id",
                keyValue: 2,
                column: "name",
                value: "אזור המרכז");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerResponses_CaseStudyId",
                table: "CustomerResponses",
                column: "CaseStudyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerResponses");

            migrationBuilder.DropColumn(
                name: "IfMainPicture",
                table: "CaseStudyPictures");

            migrationBuilder.AddColumn<string>(
                name: "CustomerResponses",
                table: "CaseStudies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Area",
                keyColumn: "Id",
                keyValue: 2,
                column: "name",
                value: "איזור המרכז");
        }
    }
}
