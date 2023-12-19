using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class caseStudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IfDisplayedInCS",
                table: "PaidTransactions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IfDisplayedInCS",
                table: "JointProjects",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Buisness",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IfDisplayedInCS",
                table: "BarterDeals",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email");

            migrationBuilder.CreateTable(
                name: "CaseStudies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromTable = table.Column<int>(nullable: false),
                    PaidTransactionID = table.Column<int>(nullable: true),
                    BarterDealID = table.Column<int>(nullable: true),
                    JointProjectID = table.Column<int>(nullable: true),
                    ReportDate = table.Column<DateTime>(nullable: false),
                    MarketingTitle = table.Column<string>(nullable: true),
                    BusinessTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Challenge = table.Column<string>(nullable: true),
                    PowerMultiplier = table.Column<string>(nullable: true),
                    CustomersGain = table.Column<string>(nullable: true),
                    CustomerResponses = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseStudies_BarterDeals_BarterDealID",
                        column: x => x.BarterDealID,
                        principalTable: "BarterDeals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseStudies_JointProjects_JointProjectID",
                        column: x => x.JointProjectID,
                        principalTable: "JointProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseStudies_PaidTransactions_PaidTransactionID",
                        column: x => x.PaidTransactionID,
                        principalTable: "PaidTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessesInCaseStudy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseStudyId = table.Column<int>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false),
                    BuinessOwnerNameForCS = table.Column<string>(nullable: true),
                    LineOfBusiness = table.Column<string>(nullable: true),
                    WordOfPartner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessesInCaseStudy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessesInCaseStudy_Buisness_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessesInCaseStudy_CaseStudies_CaseStudyId",
                        column: x => x.CaseStudyId,
                        principalTable: "CaseStudies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseStudyPictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseStudyId = table.Column<int>(nullable: false),
                    PicGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStudyPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseStudyPictures_CaseStudies_CaseStudyId",
                        column: x => x.CaseStudyId,
                        principalTable: "CaseStudies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buisness_userId",
                table: "Buisness",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessesInCaseStudy_BusinessId",
                table: "BusinessesInCaseStudy",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessesInCaseStudy_CaseStudyId",
                table: "BusinessesInCaseStudy",
                column: "CaseStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStudies_BarterDealID",
                table: "CaseStudies",
                column: "BarterDealID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStudies_JointProjectID",
                table: "CaseStudies",
                column: "JointProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStudies_PaidTransactionID",
                table: "CaseStudies",
                column: "PaidTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStudyPictures_CaseStudyId",
                table: "CaseStudyPictures",
                column: "CaseStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buisness_AspNetUsers_userId",
                table: "Buisness",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buisness_AspNetUsers_userId",
                table: "Buisness");

            migrationBuilder.DropTable(
                name: "BusinessesInCaseStudy");

            migrationBuilder.DropTable(
                name: "CaseStudyPictures");

            migrationBuilder.DropTable(
                name: "CaseStudies");

            migrationBuilder.DropIndex(
                name: "IX_Buisness_userId",
                table: "Buisness");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IfDisplayedInCS",
                table: "PaidTransactions");

            migrationBuilder.DropColumn(
                name: "IfDisplayedInCS",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "IfDisplayedInCS",
                table: "BarterDeals");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Buisness",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }
    }
}
