using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiretorServico",
                columns: table => new
                {
                    DiretorServicoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    NumeroMecanografico = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: true),
                    Morada = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiretorServico", x => x.DiretorServicoID);
                });

            migrationBuilder.CreateTable(
                name: "EspecialidadesEnfermeiros",
                columns: table => new
                {
                    EspecialidadeEnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Especialidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadesEnfermeiros", x => x.EspecialidadeEnfermeiroId);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    MedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroMecanografico = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Especialidade = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: true),
                    Data_Nascimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.MedicoId);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    PacienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Morada = table.Column<string>(nullable: true),
                    Cod_Postal = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: true),
                    Data_Nascimento = table.Column<DateTime>(nullable: false),
                    Numero_Utente = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: false),
                    BoletimClinico = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteId);
                });

            migrationBuilder.CreateTable(
                name: "Enfermeiros",
                columns: table => new
                {
                    EnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroMecanografico = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    EspecialidadeEnfermeiroId = table.Column<int>(nullable: false),
                    Contacto = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Data_Nascimento = table.Column<DateTime>(nullable: false),
                    CC = table.Column<string>(nullable: false),
                    Filhos = table.Column<bool>(nullable: false),
                    Data_Nascimento_Filho = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeiros", x => x.EnfermeiroId);
                    table.ForeignKey(
                        name: "FK_Enfermeiros_EspecialidadesEnfermeiros_EspecialidadeEnfermeiroId",
                        column: x => x.EspecialidadeEnfermeiroId,
                        principalTable: "EspecialidadesEnfermeiros",
                        principalColumn: "EspecialidadeEnfermeiroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicoEspecialidade",
                columns: table => new
                {
                    MedicoEspecialidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    MedicoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoEspecialidade", x => x.MedicoEspecialidadeId);
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidade_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnfermeirosEspecialidades",
                columns: table => new
                {
                    EnfermeiroEspecialidadeId = table.Column<int>(nullable: false),
                    EspecialidadeEnfermeiroId = table.Column<int>(nullable: false),
                    EnfermeiroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnfermeirosEspecialidades", x => new { x.EnfermeiroId, x.EspecialidadeEnfermeiroId });
                    table.ForeignKey(
                        name: "FK_EnfermeirosEspecialidades_Enfermeiros_EnfermeiroId",
                        column: x => x.EnfermeiroId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeiroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnfermeirosEspecialidades_EspecialidadesEnfermeiros_EspecialidadeEnfermeiroId",
                        column: x => x.EspecialidadeEnfermeiroId,
                        principalTable: "EspecialidadesEnfermeiros",
                        principalColumn: "EspecialidadeEnfermeiroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeiros_EspecialidadeEnfermeiroId",
                table: "Enfermeiros",
                column: "EspecialidadeEnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_EnfermeirosEspecialidades_EspecialidadeEnfermeiroId",
                table: "EnfermeirosEspecialidades",
                column: "EspecialidadeEnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoEspecialidade_MedicoId",
                table: "MedicoEspecialidade",
                column: "MedicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiretorServico");

            migrationBuilder.DropTable(
                name: "EnfermeirosEspecialidades");

            migrationBuilder.DropTable(
                name: "MedicoEspecialidade");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Enfermeiros");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "EspecialidadesEnfermeiros");
        }
    }
}
