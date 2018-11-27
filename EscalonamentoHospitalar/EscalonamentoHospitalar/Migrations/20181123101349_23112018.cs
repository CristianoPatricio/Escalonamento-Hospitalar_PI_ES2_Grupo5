using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class _23112018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Morada",
                table: "DiretorServico");

            migrationBuilder.RenameColumn(
                name: "NumeroMecanografico",
                table: "DiretorServico",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DiretorServico",
                newName: "Codigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "DiretorServico",
                newName: "NumeroMecanografico");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "DiretorServico",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Morada",
                table: "DiretorServico",
                nullable: true);
        }
    }
}
