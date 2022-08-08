using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceP.Migrations
{
    public partial class BookingOnlyNeedsCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_provideruserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_provideruserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "provideruserId",
                table: "Bookings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "provideruserId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_provideruserId",
                table: "Bookings",
                column: "provideruserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_provideruserId",
                table: "Bookings",
                column: "provideruserId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
