using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBookingWeb.Migrations
{
    public partial class PublishingDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nume",
                table: "BarberShop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nume",
                table: "BarberShop");
        }
    }
}
