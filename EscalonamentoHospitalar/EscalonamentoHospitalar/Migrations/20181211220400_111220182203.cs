using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class _111220182203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiro_Enfermeiros_EnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiro_EstadoPedidoTrocas_EstadoPedidoTrocaId",
                table: "PedidoTrocaTurnosEnfermeiro");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiro_HorariosEnfermeiro_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoTrocaTurnosEnfermeiro",
                table: "PedidoTrocaTurnosEnfermeiro");

            migrationBuilder.DropIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiro_EnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro");

            migrationBuilder.DropColumn(
                name: "EnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro");

            migrationBuilder.RenameTable(
                name: "PedidoTrocaTurnosEnfermeiro",
                newName: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiro_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                newName: "IX_PedidoTrocaTurnosEnfermeiros_HorarioEnfermeiroId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiro_EstadoPedidoTrocaId",
                table: "PedidoTrocaTurnosEnfermeiros",
                newName: "IX_PedidoTrocaTurnosEnfermeiros_EstadoPedidoTrocaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoTrocaTurnosEnfermeiros",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "PedidoTrocaTurnosEnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiros_EnfermeiroRequerenteId",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "EnfermeiroRequerenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_Enfermeiros_EnfermeiroRequerenteId",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "EnfermeiroRequerenteId",
                principalTable: "Enfermeiros",
                principalColumn: "EnfermeiroId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_EstadoPedidoTrocas_EstadoPedidoTrocaId",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "EstadoPedidoTrocaId",
                principalTable: "EstadoPedidoTrocas",
                principalColumn: "EstadoPedidoTrocaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_HorariosEnfermeiro_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "HorarioEnfermeiroId",
                principalTable: "HorariosEnfermeiro",
                principalColumn: "HorarioEnfermeiroId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_Enfermeiros_EnfermeiroRequerenteId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_EstadoPedidoTrocas_EstadoPedidoTrocaId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_HorariosEnfermeiro_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoTrocaTurnosEnfermeiros",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiros_EnfermeiroRequerenteId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.RenameTable(
                name: "PedidoTrocaTurnosEnfermeiros",
                newName: "PedidoTrocaTurnosEnfermeiro");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiros_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro",
                newName: "IX_PedidoTrocaTurnosEnfermeiro_HorarioEnfermeiroId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiros_EstadoPedidoTrocaId",
                table: "PedidoTrocaTurnosEnfermeiro",
                newName: "IX_PedidoTrocaTurnosEnfermeiro_EstadoPedidoTrocaId");

            migrationBuilder.AddColumn<int>(
                name: "EnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoTrocaTurnosEnfermeiro",
                table: "PedidoTrocaTurnosEnfermeiro",
                column: "PedidoTrocaTurnosEnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiro_EnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro",
                column: "EnfermeiroId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiro_Enfermeiros_EnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro",
                column: "EnfermeiroId",
                principalTable: "Enfermeiros",
                principalColumn: "EnfermeiroId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiro_EstadoPedidoTrocas_EstadoPedidoTrocaId",
                table: "PedidoTrocaTurnosEnfermeiro",
                column: "EstadoPedidoTrocaId",
                principalTable: "EstadoPedidoTrocas",
                principalColumn: "EstadoPedidoTrocaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiro_HorariosEnfermeiro_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiro",
                column: "HorarioEnfermeiroId",
                principalTable: "HorariosEnfermeiro",
                principalColumn: "HorarioEnfermeiroId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
