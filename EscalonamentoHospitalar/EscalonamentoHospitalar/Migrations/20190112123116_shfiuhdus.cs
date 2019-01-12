using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class shfiuhdus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricoTratamento",
                columns: table => new
                {
                    HistoricoTratamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TratamentoId = table.Column<int>(nullable: false),
                    PacienteId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoTratamento", x => x.HistoricoTratamentoId);
                    table.ForeignKey(
                        name: "FK_HistoricoTratamento_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HistoricoTratamento_Tratamentos_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamentos",
                        principalColumn: "TratamentoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTratamento_PacienteId",
                table: "HistoricoTratamento",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTratamento_TratamentoId",
                table: "HistoricoTratamento",
                column: "TratamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoTratamento");
        }
    }
}
