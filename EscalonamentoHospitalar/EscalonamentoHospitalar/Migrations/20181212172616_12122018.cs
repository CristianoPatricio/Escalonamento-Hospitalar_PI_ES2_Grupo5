using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class _12122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_HorariosEnfermeiro_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropColumn(
                name: "EnfermeiroATrocarId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropColumn(
                name: "EnfermeiroParaTrocaId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.RenameColumn(
                name: "HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                newName: "HorarioParaTrocaEnfermeiroId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiros_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                newName: "IX_PedidoTrocaTurnosEnfermeiros_HorarioParaTrocaEnfermeiroId");

            migrationBuilder.AddColumn<int>(
                name: "HorarioATrocarEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HorarioATrocarEnfermeiros",
                columns: table => new
                {
                    HorarioATrocarEnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HorarioEnfermeiroId = table.Column<int>(nullable: true),
                    HorarioATrocarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioATrocarEnfermeiros", x => x.HorarioATrocarEnfermeiroId);
                    table.ForeignKey(
                        name: "FK_HorarioATrocarEnfermeiros_HorariosEnfermeiro_HorarioEnfermeiroId",
                        column: x => x.HorarioEnfermeiroId,
                        principalTable: "HorariosEnfermeiro",
                        principalColumn: "HorarioEnfermeiroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HorarioParaTrocaEnfermeiros",
                columns: table => new
                {
                    HorarioParaTrocaEnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HorarioEnfermeiroId = table.Column<int>(nullable: true),
                    HorarioParaTrocaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioParaTrocaEnfermeiros", x => x.HorarioParaTrocaEnfermeiroId);
                    table.ForeignKey(
                        name: "FK_HorarioParaTrocaEnfermeiros_HorariosEnfermeiro_HorarioEnfermeiroId",
                        column: x => x.HorarioEnfermeiroId,
                        principalTable: "HorariosEnfermeiro",
                        principalColumn: "HorarioEnfermeiroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiros_HorarioATrocarEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "HorarioATrocarEnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioATrocarEnfermeiros_HorarioEnfermeiroId",
                table: "HorarioATrocarEnfermeiros",
                column: "HorarioEnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioParaTrocaEnfermeiros_HorarioEnfermeiroId",
                table: "HorarioParaTrocaEnfermeiros",
                column: "HorarioEnfermeiroId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_HorarioATrocarEnfermeiros_HorarioATrocarEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "HorarioATrocarEnfermeiroId",
                principalTable: "HorarioATrocarEnfermeiros",
                principalColumn: "HorarioATrocarEnfermeiroId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_HorarioParaTrocaEnfermeiros_HorarioParaTrocaEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "HorarioParaTrocaEnfermeiroId",
                principalTable: "HorarioParaTrocaEnfermeiros",
                principalColumn: "HorarioParaTrocaEnfermeiroId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_HorarioATrocarEnfermeiros_HorarioATrocarEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_HorarioParaTrocaEnfermeiros_HorarioParaTrocaEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropTable(
                name: "HorarioATrocarEnfermeiros");

            migrationBuilder.DropTable(
                name: "HorarioParaTrocaEnfermeiros");

            migrationBuilder.DropIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiros_HorarioATrocarEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.DropColumn(
                name: "HorarioATrocarEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros");

            migrationBuilder.RenameColumn(
                name: "HorarioParaTrocaEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                newName: "HorarioEnfermeiroId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoTrocaTurnosEnfermeiros_HorarioParaTrocaEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                newName: "IX_PedidoTrocaTurnosEnfermeiros_HorarioEnfermeiroId");

            migrationBuilder.AddColumn<int>(
                name: "EnfermeiroATrocarId",
                table: "PedidoTrocaTurnosEnfermeiros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnfermeiroParaTrocaId",
                table: "PedidoTrocaTurnosEnfermeiros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoTrocaTurnosEnfermeiros_HorariosEnfermeiro_HorarioEnfermeiroId",
                table: "PedidoTrocaTurnosEnfermeiros",
                column: "HorarioEnfermeiroId",
                principalTable: "HorariosEnfermeiro",
                principalColumn: "HorarioEnfermeiroId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
