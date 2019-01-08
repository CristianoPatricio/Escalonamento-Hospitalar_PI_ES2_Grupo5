using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class pedidoTrocasM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorarioATrocarMedico",
                columns: table => new
                {
                    HorarioATrocarMedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HorarioMedicoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioATrocarMedico", x => x.HorarioATrocarMedicoId);
                    table.ForeignKey(
                        name: "FK_HorarioATrocarMedico_HorariosMedicos_HorarioMedicoId",
                        column: x => x.HorarioMedicoId,
                        principalTable: "HorariosMedicos",
                        principalColumn: "HorarioMedicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorarioParaTrocaMedico",
                columns: table => new
                {
                    HorarioParaTrocaMedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HorarioMedicoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioParaTrocaMedico", x => x.HorarioParaTrocaMedicoId);
                    table.ForeignKey(
                        name: "FK_HorarioParaTrocaMedico_HorariosMedicos_HorarioMedicoId",
                        column: x => x.HorarioMedicoId,
                        principalTable: "HorariosMedicos",
                        principalColumn: "HorarioMedicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoTrocaTurnosMedico",
                columns: table => new
                {
                    PedidoTrocaTurnosMedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPedido = table.Column<DateTime>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false),
                    HorarioATrocarMedicoId = table.Column<int>(nullable: false),
                    HorarioParaTrocaMedicoId = table.Column<int>(nullable: false),
                    EstadoPedidoTrocaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoTrocaTurnosMedico", x => x.PedidoTrocaTurnosMedicoId);
                    table.ForeignKey(
                        name: "FK_PedidoTrocaTurnosMedico_EstadoPedidoTrocas_EstadoPedidoTrocaId",
                        column: x => x.EstadoPedidoTrocaId,
                        principalTable: "EstadoPedidoTrocas",
                        principalColumn: "EstadoPedidoTrocaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoTrocaTurnosMedico_HorarioATrocarMedico_HorarioATrocarMedicoId",
                        column: x => x.HorarioATrocarMedicoId,
                        principalTable: "HorarioATrocarMedico",
                        principalColumn: "HorarioATrocarMedicoId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PedidoTrocaTurnosMedico_HorarioParaTrocaMedico_HorarioParaTrocaMedicoId",
                        column: x => x.HorarioParaTrocaMedicoId,
                        principalTable: "HorarioParaTrocaMedico",
                        principalColumn: "HorarioParaTrocaMedicoId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PedidoTrocaTurnosMedico_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioATrocarMedico_HorarioMedicoId",
                table: "HorarioATrocarMedico",
                column: "HorarioMedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioParaTrocaMedico_HorarioMedicoId",
                table: "HorarioParaTrocaMedico",
                column: "HorarioMedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosMedico_EstadoPedidoTrocaId",
                table: "PedidoTrocaTurnosMedico",
                column: "EstadoPedidoTrocaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosMedico_HorarioATrocarMedicoId",
                table: "PedidoTrocaTurnosMedico",
                column: "HorarioATrocarMedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosMedico_HorarioParaTrocaMedicoId",
                table: "PedidoTrocaTurnosMedico",
                column: "HorarioParaTrocaMedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosMedico_MedicoId",
                table: "PedidoTrocaTurnosMedico",
                column: "MedicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoTrocaTurnosMedico");

            migrationBuilder.DropTable(
                name: "HorarioATrocarMedico");

            migrationBuilder.DropTable(
                name: "HorarioParaTrocaMedico");
        }
    }
}
