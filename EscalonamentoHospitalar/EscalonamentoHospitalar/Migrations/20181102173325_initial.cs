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
                name: "Enfermeiro",
                columns: table => new
                {
                    EnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroMecanografico = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Especialidade = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Data_Nascimento = table.Column<DateTime>(nullable: false),
                    CC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeiro", x => x.EnfermeiroId);
                });

            migrationBuilder.CreateTable(
                name: "Especialidade",
                columns: table => new
                {
                    EspecialidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidade", x => x.EspecialidadeId);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
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
                    table.PrimaryKey("PK_Medico", x => x.MedicoId);
                });

            migrationBuilder.CreateTable(
                name: "EnfermeiroEspecialidade",
                columns: table => new
                {
                    EnfermeiroEspecialidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnfermeiroId = table.Column<int>(nullable: false),
                    EspecilidadeId = table.Column<int>(nullable: false),
                    EspecialidadeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnfermeiroEspecialidade", x => x.EnfermeiroEspecialidadeId);
                    table.ForeignKey(
                        name: "FK_EnfermeiroEspecialidade_Enfermeiro_EnfermeiroId",
                        column: x => x.EnfermeiroId,
                        principalTable: "Enfermeiro",
                        principalColumn: "EnfermeiroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnfermeiroEspecialidade_Especialidade_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidade",
                        principalColumn: "EspecialidadeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicoEspecialidade",
                columns: table => new
                {
                    MedicoEspecialidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicoId = table.Column<int>(nullable: false),
                    EspecialidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoEspecialidade", x => x.MedicoEspecialidadeId);
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidade_Especialidade_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidade",
                        principalColumn: "EspecialidadeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidade_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnfermeiroEspecialidade_EnfermeiroId",
                table: "EnfermeiroEspecialidade",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_EnfermeiroEspecialidade_EspecialidadeId",
                table: "EnfermeiroEspecialidade",
                column: "EspecialidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoEspecialidade_EspecialidadeId",
                table: "MedicoEspecialidade",
                column: "EspecialidadeId");

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
                name: "EnfermeiroEspecialidade");

            migrationBuilder.DropTable(
                name: "MedicoEspecialidade");

            migrationBuilder.DropTable(
                name: "Enfermeiro");

            migrationBuilder.DropTable(
                name: "Especialidade");

            migrationBuilder.DropTable(
                name: "Medico");
        }
    }
}
