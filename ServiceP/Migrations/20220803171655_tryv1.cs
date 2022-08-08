using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceP.Migrations
{
    public partial class tryv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brand_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    serviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    service_type = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creatoruserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.serviceId);
                    table.ForeignKey(
                        name: "FK_Services_Users_creatoruserId",
                        column: x => x.creatoruserId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    bookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceId = table.Column<int>(type: "int", nullable: false),
                    provideruserId = table.Column<int>(type: "int", nullable: false),
                    customeruserId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.bookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Services_serviceId",
                        column: x => x.serviceId,
                        principalTable: "Services",
                        principalColumn: "serviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_customeruserId",
                        column: x => x.customeruserId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_provideruserId",
                        column: x => x.provideruserId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_customeruserId",
                table: "Bookings",
                column: "customeruserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_provideruserId",
                table: "Bookings",
                column: "provideruserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_serviceId",
                table: "Bookings",
                column: "serviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_creatoruserId",
                table: "Services",
                column: "creatoruserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
