using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class initial20Nov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnfermeiroId",
                table: "Medicos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_EnfermeiroId",
                table: "Medicos",
                column: "EnfermeiroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Enfermeiros_EnfermeiroId",
                table: "Medicos",
                column: "EnfermeiroId",
                principalTable: "Enfermeiros",
                principalColumn: "EnfermeiroId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Enfermeiros_EnfermeiroId",
                table: "Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_EnfermeiroId",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "EnfermeiroId",
                table: "Medicos");
        }
    }
}
