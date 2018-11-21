using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class _211120181016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    TurnoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.TurnoId);
                });

            migrationBuilder.CreateTable(
                name: "HorariosEnfermeiro",
                columns: table => new
                {
                    HorarioEnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    HoraInicio = table.Column<DateTime>(nullable: false),
                    HoraFim = table.Column<DateTime>(nullable: false),
                    TurnoId = table.Column<int>(nullable: false),
                    EnfermeiroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosEnfermeiro", x => x.HorarioEnfermeiroId);
                    table.ForeignKey(
                        name: "FK_HorariosEnfermeiro_Enfermeiros_EnfermeiroId",
                        column: x => x.EnfermeiroId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeiroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorariosEnfermeiro_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorariosEnfermeiro_EnfermeiroId",
                table: "HorariosEnfermeiro",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosEnfermeiro_TurnoId",
                table: "HorariosEnfermeiro",
                column: "TurnoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorariosEnfermeiro");

            migrationBuilder.DropTable(
                name: "Turnos");
        }
    }
}
