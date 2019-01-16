using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class deleteFieldHorarioPaciente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorariosPaciente_Tratamentos_TratamentoId",
                table: "HorariosPaciente");

            migrationBuilder.DropIndex(
                name: "IX_HorariosPaciente_TratamentoId",
                table: "HorariosPaciente");

            migrationBuilder.DropColumn(
                name: "TratamentoId",
                table: "HorariosPaciente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TratamentoId",
                table: "HorariosPaciente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HorariosPaciente_TratamentoId",
                table: "HorariosPaciente",
                column: "TratamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HorariosPaciente_Tratamentos_TratamentoId",
                table: "HorariosPaciente",
                column: "TratamentoId",
                principalTable: "Tratamentos",
                principalColumn: "TratamentoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
