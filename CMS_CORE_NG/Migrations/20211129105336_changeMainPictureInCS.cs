using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class changeMainPictureInCS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IfMainPicture",
                table: "CaseStudyPictures");

            migrationBuilder.AddColumn<Guid>(
                name: "PicGuid",
                table: "CaseStudies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicGuid",
                table: "CaseStudies");

            migrationBuilder.AddColumn<bool>(
                name: "IfMainPicture",
                table: "CaseStudyPictures",
                type: "bit",
                nullable: true);
        }
    }
}
