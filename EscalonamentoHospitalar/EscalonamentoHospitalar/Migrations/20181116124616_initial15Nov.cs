using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class initial15Nov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Regime",
                table: "Tratamento");

            migrationBuilder.AddColumn<int>(
                name: "RegimeId",
                table: "Tratamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_RegimeId",
                table: "Tratamento",
                column: "RegimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tratamento_Regime_RegimeId",
                table: "Tratamento",
                column: "RegimeId",
                principalTable: "Regime",
                principalColumn: "RegimeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tratamento_Regime_RegimeId",
                table: "Tratamento");

            migrationBuilder.DropIndex(
                name: "IX_Tratamento_RegimeId",
                table: "Tratamento");

            migrationBuilder.DropColumn(
                name: "RegimeId",
                table: "Tratamento");

            migrationBuilder.AddColumn<string>(
                name: "Regime",
                table: "Tratamento",
                nullable: true);
        }
    }
}
