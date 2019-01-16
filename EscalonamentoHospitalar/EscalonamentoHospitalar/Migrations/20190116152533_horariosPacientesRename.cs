using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class horariosPacientesRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorarioPaciente_Pacientes_PacienteId",
                table: "HorarioPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_HorarioPaciente_Tratamentos_TratamentoId",
                table: "HorarioPaciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HorarioPaciente",
                table: "HorarioPaciente");

            migrationBuilder.RenameTable(
                name: "HorarioPaciente",
                newName: "HorariosPaciente");

            migrationBuilder.RenameIndex(
                name: "IX_HorarioPaciente_TratamentoId",
                table: "HorariosPaciente",
                newName: "IX_HorariosPaciente_TratamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_HorarioPaciente_PacienteId",
                table: "HorariosPaciente",
                newName: "IX_HorariosPaciente_PacienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HorariosPaciente",
                table: "HorariosPaciente",
                column: "HorarioPacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_HorariosPaciente_Pacientes_PacienteId",
                table: "HorariosPaciente",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "PacienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HorariosPaciente_Tratamentos_TratamentoId",
                table: "HorariosPaciente",
                column: "TratamentoId",
                principalTable: "Tratamentos",
                principalColumn: "TratamentoId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorariosPaciente_Pacientes_PacienteId",
                table: "HorariosPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_HorariosPaciente_Tratamentos_TratamentoId",
                table: "HorariosPaciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HorariosPaciente",
                table: "HorariosPaciente");

            migrationBuilder.RenameTable(
                name: "HorariosPaciente",
                newName: "HorarioPaciente");

            migrationBuilder.RenameIndex(
                name: "IX_HorariosPaciente_TratamentoId",
                table: "HorarioPaciente",
                newName: "IX_HorarioPaciente_TratamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_HorariosPaciente_PacienteId",
                table: "HorarioPaciente",
                newName: "IX_HorarioPaciente_PacienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HorarioPaciente",
                table: "HorarioPaciente",
                column: "HorarioPacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_HorarioPaciente_Pacientes_PacienteId",
                table: "HorarioPaciente",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "PacienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HorarioPaciente_Tratamentos_TratamentoId",
                table: "HorarioPaciente",
                column: "TratamentoId",
                principalTable: "Tratamentos",
                principalColumn: "TratamentoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
