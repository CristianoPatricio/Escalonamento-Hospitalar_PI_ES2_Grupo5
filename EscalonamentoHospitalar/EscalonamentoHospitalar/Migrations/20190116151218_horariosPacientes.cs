using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class horariosPacientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorarioPaciente",
                columns: table => new
                {
                    HorarioPacienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    Duracao = table.Column<TimeSpan>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    PacienteId = table.Column<int>(nullable: false),
                    TratamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioPaciente", x => x.HorarioPacienteId);
                    table.ForeignKey(
                        name: "FK_HorarioPaciente_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorarioPaciente_Tratamentos_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamentos",
                        principalColumn: "TratamentoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioPaciente_PacienteId",
                table: "HorarioPaciente",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioPaciente_TratamentoId",
                table: "HorarioPaciente",
                column: "TratamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioPaciente");
        }
    }
}
