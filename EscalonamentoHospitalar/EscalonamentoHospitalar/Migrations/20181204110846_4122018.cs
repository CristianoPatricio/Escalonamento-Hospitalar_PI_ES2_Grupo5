using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscalonamentoHospitalar.Migrations
{
    public partial class _4122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiretoresServico",
                columns: table => new
                {
                    DiretorServicoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Contacto = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CC = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiretoresServico", x => x.DiretorServicoID);
                });

            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    EquipamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.EquipamentoId);
                });

            migrationBuilder.CreateTable(
                name: "EspecialidadeMedicos",
                columns: table => new
                {
                    EspecialidadeMedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeEspecialidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadeMedicos", x => x.EspecialidadeMedicoId);
                });

            migrationBuilder.CreateTable(
                name: "EspecialidadesEnfermeiros",
                columns: table => new
                {
                    EspecialidadeEnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Especialidade = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadesEnfermeiros", x => x.EspecialidadeEnfermeiroId);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    EstadoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.EstadoId);
                });

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
                name: "Pacientes",
                columns: table => new
                {
                    PacienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    Cod_Postal = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: false),
                    Data_Nascimento = table.Column<DateTime>(nullable: false),
                    Numero_Utente = table.Column<string>(nullable: false),
                    Contacto = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteId);
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
                name: "Regime",
                columns: table => new
                {
                    RegimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoRegime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regime", x => x.RegimeId);
                });

            migrationBuilder.CreateTable(
                name: "Regras",
                columns: table => new
                {
                    RegraId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegrasEscalonamento = table.Column<string>(nullable: true),
                    Numero = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regras", x => x.RegraId);
                });

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
                name: "Medicos",
                columns: table => new
                {
                    MedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroMecanografico = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: false),
                    Data_Nascimento = table.Column<DateTime>(nullable: false),
                    EspecialidadeMedicoId = table.Column<int>(nullable: false),
                    Data_Inicio_Servico = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.MedicoId);
                    table.ForeignKey(
                        name: "FK_Medicos_EspecialidadeMedicos_EspecialidadeMedicoId",
                        column: x => x.EspecialidadeMedicoId,
                        principalTable: "EspecialidadeMedicos",
                        principalColumn: "EspecialidadeMedicoId",
                        onDelete: ReferentialAction.Cascade);
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
                    Contacto = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Data_Nascimento = table.Column<DateTime>(nullable: false),
                    CC = table.Column<string>(nullable: false),
                    Filhos = table.Column<bool>(nullable: false),
                    Data_Nascimento_Filho = table.Column<DateTime>(nullable: true)
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
                name: "EscalaMedicos",
                columns: table => new
                {
                    EscalaMedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TurnoId = table.Column<int>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false),
                    Data_Inicio_Semana = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalaMedicos", x => x.EscalaMedicoId);
                    table.ForeignKey(
                        name: "FK_EscalaMedicos_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscalaMedicos_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicoEspecialidades",
                columns: table => new
                {
                    MedicoEspecialidadeId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    MedicoId = table.Column<int>(nullable: false),
                    EspecialidadeMedicoId = table.Column<int>(nullable: false),
                    Data_Registo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoEspecialidades", x => new { x.MedicoId, x.EspecialidadeMedicoId });
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidades_EspecialidadeMedicos_EspecialidadeMedicoId",
                        column: x => x.EspecialidadeMedicoId,
                        principalTable: "EspecialidadeMedicos",
                        principalColumn: "EspecialidadeMedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidades_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tratamentos",
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
                    RegimeId = table.Column<int>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamentos", x => x.TratamentoId);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "EstadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Grau_GrauId",
                        column: x => x.GrauId,
                        principalTable: "Grau",
                        principalColumn: "GrauId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Patologia_PatologiaId",
                        column: x => x.PatologiaId,
                        principalTable: "Patologia",
                        principalColumn: "PatologiaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Regime_RegimeId",
                        column: x => x.RegimeId,
                        principalTable: "Regime",
                        principalColumn: "RegimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnfermeirosEspecialidades",
                columns: table => new
                {
                    EspecialidadeEnfermeiroId = table.Column<int>(nullable: false),
                    EnfermeiroId = table.Column<int>(nullable: false),
                    Data_Registo = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "EscalaEnfermeiros",
                columns: table => new
                {
                    EscalaEnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TurnoId = table.Column<int>(nullable: false),
                    EnfermeiroId = table.Column<int>(nullable: false),
                    Data_Inicio_Semana = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalaEnfermeiros", x => x.EscalaEnfermeiroId);
                    table.ForeignKey(
                        name: "FK_EscalaEnfermeiros_Enfermeiros_EnfermeiroId",
                        column: x => x.EnfermeiroId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeiroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscalaEnfermeiros_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorariosEnfermeiro",
                columns: table => new
                {
                    HorarioEnfermeiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInicioTurno = table.Column<DateTime>(nullable: false),
                    Duracao = table.Column<int>(nullable: false),
                    DataFimTurno = table.Column<DateTime>(nullable: false),
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
                name: "IX_Enfermeiros_EspecialidadeEnfermeiroId",
                table: "Enfermeiros",
                column: "EspecialidadeEnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_EnfermeirosEspecialidades_EspecialidadeEnfermeiroId",
                table: "EnfermeirosEspecialidades",
                column: "EspecialidadeEnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalaEnfermeiros_EnfermeiroId",
                table: "EscalaEnfermeiros",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalaEnfermeiros_TurnoId",
                table: "EscalaEnfermeiros",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalaMedicos_MedicoId",
                table: "EscalaMedicos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalaMedicos_TurnoId",
                table: "EscalaMedicos",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosEnfermeiro_EnfermeiroId",
                table: "HorariosEnfermeiro",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosEnfermeiro_TurnoId",
                table: "HorariosEnfermeiro",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoEspecialidades_EspecialidadeMedicoId",
                table: "MedicoEspecialidades",
                column: "EspecialidadeMedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_EspecialidadeMedicoId",
                table: "Medicos",
                column: "EspecialidadeMedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_EstadoId",
                table: "Tratamentos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_GrauId",
                table: "Tratamentos",
                column: "GrauId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_MedicoId",
                table: "Tratamentos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_PacienteId",
                table: "Tratamentos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_PatologiaId",
                table: "Tratamentos",
                column: "PatologiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_RegimeId",
                table: "Tratamentos",
                column: "RegimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiretoresServico");

            migrationBuilder.DropTable(
                name: "EnfermeirosEspecialidades");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "EscalaEnfermeiros");

            migrationBuilder.DropTable(
                name: "EscalaMedicos");

            migrationBuilder.DropTable(
                name: "HorariosEnfermeiro");

            migrationBuilder.DropTable(
                name: "MedicoEspecialidades");

            migrationBuilder.DropTable(
                name: "Regras");

            migrationBuilder.DropTable(
                name: "Tratamentos");

            migrationBuilder.DropTable(
                name: "Enfermeiros");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Grau");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Patologia");

            migrationBuilder.DropTable(
                name: "Regime");

            migrationBuilder.DropTable(
                name: "EspecialidadesEnfermeiros");

            migrationBuilder.DropTable(
                name: "EspecialidadeMedicos");
        }
    }
}
