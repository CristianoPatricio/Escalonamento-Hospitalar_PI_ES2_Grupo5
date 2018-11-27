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
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
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

                    b.Property<string>("Contacto");

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

                    b.Property<string>("Contacto");

                    b.Property<DateTime>("Data_Nascimento");

                    b.Property<DateTime?>("Data_Nascimento_Filho");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("EspecialidadeEnfermeiroId");

                    b.Property<bool?>("Filhos")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

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

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Grau", b =>
                {
                    b.Property<int>("GrauId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("CC");

                    b.Property<string>("Contacto");

                    b.Property<DateTime>("Data_Nascimento");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<string>("NumeroMecanografico");

                    b.HasKey("MedicoId");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.MedicoEspecialidade", b =>
                {
                    b.Property<int>("MedicoEspecialidadeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MedicoId");

                    b.Property<string>("Nome");

                    b.HasKey("MedicoEspecialidadeId");

                    b.HasIndex("MedicoId");

                    b.ToTable("MedicoEspecialidade");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Paciente", b =>
                {
                    b.Property<int>("PacienteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CC");

                    b.Property<string>("Cod_Postal");

                    b.Property<string>("Contacto")
                        .IsRequired();

                    b.Property<DateTime>("Data_Nascimento");

                    b.Property<string>("Email");

                    b.Property<string>("Morada");

                    b.Property<string>("Nome");

                    b.Property<string>("Numero_Utente");

                    b.HasKey("PacienteId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Patologia", b =>
                {
                    b.Property<int>("PatologiaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("PatologiaId");

                    b.ToTable("Patologia");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Tratamento", b =>
                {
                    b.Property<int>("TratamentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Concluido");

                    b.Property<DateTime>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<bool>("Decorrer");

                    b.Property<string>("DuracaoCiclo")
                        .IsRequired();

                    b.Property<int>("GrauId");

                    b.Property<int>("PacienteId");

                    b.Property<int>("PatologiaId");

                    b.Property<string>("Regime");

                    b.HasKey("TratamentoId");

                    b.HasIndex("GrauId");

                    b.HasIndex("PacienteId");

                    b.HasIndex("PatologiaId");

                    b.ToTable("Tratamento");
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

            modelBuilder.Entity("EscalonamentoHospitalar.Models.MedicoEspecialidade", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.Medico", "Medico")
                        .WithMany("MedicoEspecialidade")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Tratamento", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.Grau", "Grau")
                        .WithMany()
                        .HasForeignKey("GrauId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscalonamentoHospitalar.Models.Patologia", "Patologia")
                        .WithMany()
                        .HasForeignKey("PatologiaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
