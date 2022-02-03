using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteJu.Migrations.Reservation
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Firstname = table.Column<string>(type: "TEXT", nullable: true),
                    Lastname = table.Column<string>(type: "TEXT", nullable: true),
                    Telephone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prestation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prestations = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RDV",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Heure = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    Firstname = table.Column<string>(type: "TEXT", nullable: true),
                    Name_prestaitons = table.Column<string>(type: "TEXT", nullable: true),
                    ClientID = table.Column<int>(type: "INTEGER", nullable: true),
                    PrestationID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RDV", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RDV_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RDV_Prestation_PrestationID",
                        column: x => x.PrestationID,
                        principalTable: "Prestation",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RDV_ClientID",
                table: "RDV",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_RDV_PrestationID",
                table: "RDV",
                column: "PrestationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RDV");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Prestation");
        }
    }
}
