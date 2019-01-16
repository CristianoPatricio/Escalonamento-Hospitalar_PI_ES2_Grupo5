using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class _2119 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorarioMedico_Medicos_MedicoId",
                table: "HorarioMedico");

            migrationBuilder.DropForeignKey(
                name: "FK_HorarioMedico_Turnos_TurnoId",
                table: "HorarioMedico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HorarioMedico",
                table: "HorarioMedico");

            migrationBuilder.RenameTable(
                name: "HorarioMedico",
                newName: "HorariosMedicos");

            migrationBuilder.RenameIndex(
                name: "IX_HorarioMedico_TurnoId",
                table: "HorariosMedicos",
                newName: "IX_HorariosMedicos_TurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_HorarioMedico_MedicoId",
                table: "HorariosMedicos",
                newName: "IX_HorariosMedicos_MedicoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HorariosMedicos",
                table: "HorariosMedicos",
                column: "HorarioMedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HorariosMedicos_Medicos_MedicoId",
                table: "HorariosMedicos",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "MedicoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HorariosMedicos_Turnos_TurnoId",
                table: "HorariosMedicos",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "TurnoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorariosMedicos_Medicos_MedicoId",
                table: "HorariosMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_HorariosMedicos_Turnos_TurnoId",
                table: "HorariosMedicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HorariosMedicos",
                table: "HorariosMedicos");

            migrationBuilder.RenameTable(
                name: "HorariosMedicos",
                newName: "HorarioMedico");

            migrationBuilder.RenameIndex(
                name: "IX_HorariosMedicos_TurnoId",
                table: "HorarioMedico",
                newName: "IX_HorarioMedico_TurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_HorariosMedicos_MedicoId",
                table: "HorarioMedico",
                newName: "IX_HorarioMedico_MedicoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HorarioMedico",
                table: "HorarioMedico",
                column: "HorarioMedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HorarioMedico_Medicos_MedicoId",
                table: "HorarioMedico",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "MedicoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HorarioMedico_Turnos_TurnoId",
                table: "HorarioMedico",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "TurnoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
