using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteJu.Migrations.Reservation
{
    public partial class Duration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RDVS_Clients_ClientID",
                table: "RDVS");

            migrationBuilder.DropForeignKey(
                name: "FK_RDVS_Prestations_PrestationID",
                table: "RDVS");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RDVS");

            migrationBuilder.DropColumn(
                name: "Heure",
                table: "RDVS");

            migrationBuilder.RenameColumn(
                name: "PrestationID",
                table: "RDVS",
                newName: "PrestationId");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "RDVS",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "RDVS",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_RDVS_PrestationID",
                table: "RDVS",
                newName: "IX_RDVS_PrestationId");

            migrationBuilder.RenameIndex(
                name: "IX_RDVS_ClientID",
                table: "RDVS",
                newName: "IX_RDVS_ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "PrestationId",
                table: "RDVS",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "RDVS",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "At",
                table: "RDVS",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Prestations",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_RDVS_Clients_ClientId",
                table: "RDVS",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RDVS_Prestations_PrestationId",
                table: "RDVS",
                column: "PrestationId",
                principalTable: "Prestations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RDVS_Clients_ClientId",
                table: "RDVS");

            migrationBuilder.DropForeignKey(
                name: "FK_RDVS_Prestations_PrestationId",
                table: "RDVS");

            migrationBuilder.DropColumn(
                name: "At",
                table: "RDVS");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Prestations");

            migrationBuilder.RenameColumn(
                name: "PrestationId",
                table: "RDVS",
                newName: "PrestationID");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "RDVS",
                newName: "ClientID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RDVS",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_RDVS_PrestationId",
                table: "RDVS",
                newName: "IX_RDVS_PrestationID");

            migrationBuilder.RenameIndex(
                name: "IX_RDVS_ClientId",
                table: "RDVS",
                newName: "IX_RDVS_ClientID");

            migrationBuilder.AlterColumn<int>(
                name: "PrestationID",
                table: "RDVS",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "RDVS",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "RDVS",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Heure",
                table: "RDVS",
                type: "TEXT",
                nullable: true);

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
    }
}
