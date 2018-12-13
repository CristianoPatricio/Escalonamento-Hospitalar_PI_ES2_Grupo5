using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class _111220181241 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoTrocaTurnosEnfermeiro",
                columns: table => new
                {
                    PedidoTrocaTurnosEnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPedido = table.Column<DateTime>(nullable: false),
                    EnfermeiroId = table.Column<int>(nullable: true),
                    EnfermeiroRequerenteId = table.Column<int>(nullable: false),
                    HorarioEnfermeiroId = table.Column<int>(nullable: true),
                    HorarioATrocarId = table.Column<int>(nullable: false),
                    EnfermeiroATrocarId = table.Column<int>(nullable: false),
                    HorarioParaTrocaId = table.Column<int>(nullable: false),
                    EnfermeiroParaTrocaId = table.Column<int>(nullable: false),
                    EstadoPedidoTrocaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoTrocaTurnosEnfermeiro", x => x.PedidoTrocaTurnosEnfermeiroId);
                    table.ForeignKey(
                        name: "FK_PedidoTrocaTurnosEnfermeiro_Enfermeiros_EnfermeiroId",
                        column: x => x.EnfermeiroId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeiroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PedidoTrocaTurnosEnfermeiro_EstadoPedidoTrocas_EstadoPedidoTrocaId",
                        column: x => x.EstadoPedidoTrocaId,
                        principalTable: "EstadoPedidoTrocas",
                        principalColumn: "EstadoPedidoTrocaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoTrocaTurnosEnfermeiro_HorariosEnfermeiro_HorarioEnfermeiroId",
                        column: x => x.HorarioEnfermeiroId,
                        principalTable: "HorariosEnfermeiro",
                        principalColumn: "HorarioEnfermeiroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiro_EnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiro_EstadoPedidoTrocaId",
                table: "PedidoTrocaTurnosEnfermeiro",
                column: "EstadoPedidoTrocaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiro_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro",
                column: "HorarioEnfermeiroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoTrocaTurnosEnfermeiro");
        }
    }
}
