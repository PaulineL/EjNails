using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteJu.Migrations.Reservation
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RDV_Client_ClientID",
                table: "RDV");

            migrationBuilder.DropForeignKey(
                name: "FK_RDV_Prestation_PrestationID",
                table: "RDV");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RDV",
                table: "RDV");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prestation",
                table: "Prestation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "RDV");

            migrationBuilder.DropColumn(
                name: "Name_prestaitons",
                table: "RDV");

            migrationBuilder.RenameTable(
                name: "RDV",
                newName: "RDVS");

            migrationBuilder.RenameTable(
                name: "Prestation",
                newName: "Prestations");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_RDV_PrestationID",
                table: "RDVS",
                newName: "IX_RDVS_PrestationID");

            migrationBuilder.RenameIndex(
                name: "IX_RDV_ClientID",
                table: "RDVS",
                newName: "IX_RDVS_ClientID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RDVS",
                table: "RDVS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prestations",
                table: "Prestations",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RDVS_Clients_ClientID",
                table: "RDVS",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RDVS_Prestations_PrestationID",
                table: "RDVS",
                column: "PrestationID",
                principalTable: "Prestations",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RDVS_Clients_ClientID",
                table: "RDVS");

            migrationBuilder.DropForeignKey(
                name: "FK_RDVS_Prestations_PrestationID",
                table: "RDVS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RDVS",
                table: "RDVS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prestations",
                table: "Prestations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "RDVS",
                newName: "RDV");

            migrationBuilder.RenameTable(
                name: "Prestations",
                newName: "Prestation");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_RDVS_PrestationID",
                table: "RDV",
                newName: "IX_RDV_PrestationID");

            migrationBuilder.RenameIndex(
                name: "IX_RDVS_ClientID",
                table: "RDV",
                newName: "IX_RDV_ClientID");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "RDV",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_prestaitons",
                table: "RDV",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RDV",
                table: "RDV",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prestation",
                table: "Prestation",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RDV_Client_ClientID",
                table: "RDV",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RDV_Prestation_PrestationID",
                table: "RDV",
                column: "PrestationID",
                principalTable: "Prestation",
                principalColumn: "ID");
        }
    }
}
