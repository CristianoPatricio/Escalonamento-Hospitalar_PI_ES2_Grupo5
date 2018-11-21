using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class _211120181032 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "HorariosEnfermeiro");

            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "HorariosEnfermeiro");

            migrationBuilder.RenameColumn(
                name: "HoraInicio",
                table: "HorariosEnfermeiro",
                newName: "DataInicioTurno");

            migrationBuilder.AddColumn<int>(
                name: "Duracao",
                table: "HorariosEnfermeiro",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "HorariosEnfermeiro");

            migrationBuilder.RenameColumn(
                name: "DataInicioTurno",
                table: "HorariosEnfermeiro",
                newName: "HoraInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "HorariosEnfermeiro",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraFim",
                table: "HorariosEnfermeiro",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
