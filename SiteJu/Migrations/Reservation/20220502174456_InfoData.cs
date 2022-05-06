using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteJu.Migrations.Reservation
{
    public partial class InfoData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "Clients",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "Clients");
        }
    }
}
