using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class IconsFieldsInCollaboration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creativity",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "Enterprise",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "ExposureToNewAudiences",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "Professionalism",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "BarterDeals");

            migrationBuilder.DropColumn(
                name: "FairConsiderationForTransaction",
                table: "BarterDeals");

            migrationBuilder.DropColumn(
                name: "Professionalism",
                table: "BarterDeals");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "BarterDeals");

            migrationBuilder.AddColumn<bool>(
                name: "Flexable",
                table: "PaidTransactions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncreasingRevenue",
                table: "JointProjects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MoreLeisure",
                table: "JointProjects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MoreShopping",
                table: "JointProjects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReducingEffort",
                table: "JointProjects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReducingExpenses",
                table: "JointProjects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncreasingRevenue",
                table: "BarterDeals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MoreLeisure",
                table: "BarterDeals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MoreShopping",
                table: "BarterDeals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReducingEffort",
                table: "BarterDeals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReducingExpenses",
                table: "BarterDeals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flexable",
                table: "PaidTransactions");

            migrationBuilder.DropColumn(
                name: "IncreasingRevenue",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "MoreLeisure",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "MoreShopping",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "ReducingEffort",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "ReducingExpenses",
                table: "JointProjects");

            migrationBuilder.DropColumn(
                name: "IncreasingRevenue",
                table: "BarterDeals");

            migrationBuilder.DropColumn(
                name: "MoreLeisure",
                table: "BarterDeals");

            migrationBuilder.DropColumn(
                name: "MoreShopping",
                table: "BarterDeals");

            migrationBuilder.DropColumn(
                name: "ReducingEffort",
                table: "BarterDeals");

            migrationBuilder.DropColumn(
                name: "ReducingExpenses",
                table: "BarterDeals");

            migrationBuilder.AddColumn<bool>(
                name: "Creativity",
                table: "JointProjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enterprise",
                table: "JointProjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ExposureToNewAudiences",
                table: "JointProjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Professionalism",
                table: "JointProjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Availability",
                table: "BarterDeals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FairConsiderationForTransaction",
                table: "BarterDeals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Professionalism",
                table: "BarterDeals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Service",
                table: "BarterDeals",
                type: "bit",
                nullable: true);
        }
    }
}
