using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class horariosmedicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorarioMedico",
                columns: table => new
                {
                    HorarioMedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInicioTurno = table.Column<DateTime>(nullable: false),
                    Duracao = table.Column<int>(nullable: false),
                    DataFimTurno = table.Column<DateTime>(nullable: false),
                    TurnoId = table.Column<int>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioMedico", x => x.HorarioMedicoId);
                    table.ForeignKey(
                        name: "FK_HorarioMedico_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorarioMedico_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioMedico_MedicoId",
                table: "HorarioMedico",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioMedico_TurnoId",
                table: "HorarioMedico",
                column: "TurnoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioMedico");
        }
    }
}
