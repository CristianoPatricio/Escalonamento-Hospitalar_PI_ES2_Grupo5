﻿// <auto-generated />
using System;
using EscalonamentoHospitalar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EscalonamentoHospitalar.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    partial class HospitalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EscalonamentoHospitalar.Models.DiretorServico", b =>
                {
                    b.Property<int>("DiretorServicoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CC")
                        .IsRequired();

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Contacto")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("DiretorServicoID");

                    b.ToTable("DiretoresServico");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Enfermeiro", b =>
                {
                    b.Property<int>("EnfermeiroId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CC")
                        .IsRequired();

                    b.Property<string>("Contacto")
                        .IsRequired();

                    b.Property<DateTime>("Data_Nascimento");

                    b.Property<DateTime?>("Data_Nascimento_Filho");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("EspecialidadeEnfermeiroId");

                    b.Property<bool?>("Filhos")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NumeroMecanografico")
                        .IsRequired();

                    b.HasKey("EnfermeiroId");

                    b.HasIndex("EspecialidadeEnfermeiroId");

                    b.ToTable("Enfermeiros");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.EnfermeiroEspecialidade", b =>
                {
                    b.Property<int>("EnfermeiroId");

                    b.Property<int>("EspecialidadeEnfermeiroId");

                    b.Property<DateTime>("Data_Registo");

                    b.HasKey("EnfermeiroId", "EspecialidadeEnfermeiroId");

                    b.HasIndex("EspecialidadeEnfermeiroId");

                    b.ToTable("EnfermeirosEspecialidades");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.EspecialidadeEnfermeiro", b =>
                {
                    b.Property<int>("EspecialidadeEnfermeiroId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Especialidade")
                        .IsRequired();

                    b.HasKey("EspecialidadeEnfermeiroId");

                    b.ToTable("EspecialidadesEnfermeiros");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.EspecialidadeMedico", b =>
                {
                    b.Property<int>("EspecialidadeMedicoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomeEspecialidade");

                    b.HasKey("EspecialidadeMedicoId");

                    b.ToTable("EspecialidadeMedicos");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Estado", b =>
                {
                    b.Property<int>("EstadoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("EstadoId");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Grau", b =>
                {
                    b.Property<int>("GrauId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TipoGrau");

                    b.HasKey("GrauId");

                    b.ToTable("Grau");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.HorarioEnfermeiro", b =>
                {
                    b.Property<int>("HorarioEnfermeiroId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataFimTurno");

                    b.Property<DateTime>("DataInicioTurno");

                    b.Property<int>("Duracao");

                    b.Property<int>("EnfermeiroId");

                    b.Property<int>("TurnoId");

                    b.HasKey("HorarioEnfermeiroId");

                    b.HasIndex("EnfermeiroId");

                    b.HasIndex("TurnoId");

                    b.ToTable("HorariosEnfermeiro");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Medico", b =>
                {
                    b.Property<int>("MedicoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CC")
                        .IsRequired();

                    b.Property<string>("Contacto");

                    b.Property<DateTime>("Data_Inicio_Servico");

                    b.Property<DateTime>("Data_Nascimento");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("EspecialidadeMedicoId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NumeroMecanografico")
                        .IsRequired();

                    b.HasKey("MedicoId");

                    b.HasIndex("EspecialidadeMedicoId");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.MedicoEspecialidade", b =>
                {
                    b.Property<int>("MedicoId");

                    b.Property<int>("EspecialidadeMedicoId");

                    b.Property<DateTime>("Data_Registo");

                    b.HasKey("MedicoId", "EspecialidadeMedicoId");

                    b.HasIndex("EspecialidadeMedicoId");

                    b.ToTable("MedicoEspecialidades");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Paciente", b =>
                {
                    b.Property<int>("PacienteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CC")
                        .IsRequired();

                    b.Property<string>("Cod_Postal")
                        .IsRequired();

                    b.Property<string>("Contacto")
                        .IsRequired();

                    b.Property<DateTime>("Data_Nascimento");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Morada")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Numero_Utente")
                        .IsRequired();

                    b.HasKey("PacienteId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Patologia", b =>
                {
                    b.Property<int>("PatologiaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("PatologiaId");

                    b.ToTable("Patologia");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Regime", b =>
                {
                    b.Property<int>("RegimeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TipoRegime");

                    b.HasKey("RegimeId");

                    b.ToTable("Regime");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Regra", b =>
                {
                    b.Property<int>("RegraId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Numero");

                    b.Property<string>("RegrasEscalonamento");

                    b.HasKey("RegraId");

                    b.ToTable("Regras");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Tratamento", b =>
                {
                    b.Property<int>("TratamentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<string>("DuracaoCiclo")
                        .IsRequired();

                    b.Property<int>("EstadoId");

                    b.Property<int>("GrauId");

                    b.Property<int>("MedicoId");

                    b.Property<int>("PacienteId");

                    b.Property<int>("PatologiaId");

                    b.Property<int>("RegimeId");

                    b.HasKey("TratamentoId");

                    b.HasIndex("EstadoId");

                    b.HasIndex("GrauId");

                    b.HasIndex("MedicoId");

                    b.HasIndex("PacienteId");

                    b.HasIndex("PatologiaId");

                    b.HasIndex("RegimeId");

                    b.ToTable("Tratamentos");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Turno", b =>
                {
                    b.Property<int>("TurnoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("TurnoId");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Enfermeiro", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.EspecialidadeEnfermeiro", "EspecialidadeEnfermeiro")
                        .WithMany("Enfermeiro")
                        .HasForeignKey("EspecialidadeEnfermeiroId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.EnfermeiroEspecialidade", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.Enfermeiro", "Enfermeiro")
                        .WithMany("EnfermeirosEspecialidade")
                        .HasForeignKey("EnfermeiroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.EspecialidadeEnfermeiro", "EspecialidadeEnfermeiro")
                        .WithMany("EnfermeirosEspecialidade")
                        .HasForeignKey("EspecialidadeEnfermeiroId");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.HorarioEnfermeiro", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.Enfermeiro", "Enfermeiro")
                        .WithMany("HorariosEnfermeiro")
                        .HasForeignKey("EnfermeiroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.Turno", "Turno")
                        .WithMany("HorariosEnfermeiro")
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Medico", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.EspecialidadeMedico", "EspecialidadeMedico")
                        .WithMany("Medico")
                        .HasForeignKey("EspecialidadeMedicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.MedicoEspecialidade", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.EspecialidadeMedico", "EspecialidadeMedico")
                        .WithMany("MedicosEspecialidade")
                        .HasForeignKey("EspecialidadeMedicoId");

                    b.HasOne("EscalonamentoHospitalar.Models.Medico", "Medico")
                        .WithMany("MedicosEspecialidade")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Tratamento", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.Estado", "Estado")
                        .WithMany("Tratamentos")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.Grau", "Grau")
                        .WithMany("Tratamentos")
                        .HasForeignKey("GrauId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.Paciente", "Paciente")
                        .WithMany("Tratamentos")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.Patologia", "Patologia")
                        .WithMany("Tratamentos")
                        .HasForeignKey("PatologiaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.Regime", "Regime")
                        .WithMany("Tratamentos")
                        .HasForeignKey("RegimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
