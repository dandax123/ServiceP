using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceP.Migrations
{
    public partial class ServiceStatusUpdateAndNameAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "service_name",
                table: "Services",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "service_name",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
