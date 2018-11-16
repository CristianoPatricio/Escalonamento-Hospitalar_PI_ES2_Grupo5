using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class Initial16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Numero_Utente",
                table: "Pacientes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CC",
                table: "Pacientes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Grau",
                columns: table => new
                {
                    GrauId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoGrau = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grau", x => x.GrauId);
                });

            migrationBuilder.CreateTable(
                name: "Patologia",
                columns: table => new
                {
                    PatologiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patologia", x => x.PatologiaId);
                });

            migrationBuilder.CreateTable(
                name: "Tratamento",
                columns: table => new
                {
                    TratamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatologiaId = table.Column<int>(nullable: false),
                    PacienteId = table.Column<int>(nullable: false),
                    GrauId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    DuracaoCiclo = table.Column<string>(nullable: false),
                    Regime = table.Column<string>(nullable: true),
                    Decorrer = table.Column<bool>(nullable: false),
                    Concluido = table.Column<bool>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamento", x => x.TratamentoId);
                    table.ForeignKey(
                        name: "FK_Tratamento_Grau_GrauId",
                        column: x => x.GrauId,
                        principalTable: "Grau",
                        principalColumn: "GrauId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamento_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamento_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamento_Patologia_PatologiaId",
                        column: x => x.PatologiaId,
                        principalTable: "Patologia",
                        principalColumn: "PatologiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_GrauId",
                table: "Tratamento",
                column: "GrauId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_MedicoId",
                table: "Tratamento",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_PacienteId",
                table: "Tratamento",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_PatologiaId",
                table: "Tratamento",
                column: "PatologiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tratamento");

            migrationBuilder.DropTable(
                name: "Grau");

            migrationBuilder.DropTable(
                name: "Patologia");

            migrationBuilder.AlterColumn<string>(
                name: "Numero_Utente",
                table: "Pacientes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CC",
                table: "Pacientes",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
