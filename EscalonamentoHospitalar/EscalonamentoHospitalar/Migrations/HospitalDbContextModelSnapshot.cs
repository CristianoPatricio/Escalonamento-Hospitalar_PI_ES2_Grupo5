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

                    b.Property<string>("CC");

                    b.Property<string>("Contacto")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("Morada");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NumeroMecanografico");

                    b.HasKey("DiretorServicoID");

                    b.ToTable("DiretorServico");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Enfermeiro", b =>
                {
                    b.Property<int>("EnfermeiroId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CC");

                    b.Property<string>("Contacto");

                    b.Property<DateTime>("Data_Nascimento");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("NumeroMecanografico");

                    b.HasKey("EnfermeiroId");

                    b.ToTable("Enfermeiros");
                });

            modelBuilder.Entity("EscalonamentoHospitalar.Models.EnfermeiroEspecialidade", b =>
                {
                    b.Property<int>("EnfermeiroEspecialidadeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnfermeiroId");

                    b.Property<string>("Nome");

                    b.HasKey("EnfermeiroEspecialidadeId");

                    b.HasIndex("EnfermeiroId");

                    b.ToTable("EnfermeiroEspecialidades");
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

                    b.Property<string>("Email");

                    b.Property<int>("EspecialidadeMedicoId");

                    b.Property<string>("Nome")
                        .IsRequired();

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

            modelBuilder.Entity("EscalonamentoHospitalar.Models.EnfermeiroEspecialidade", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.Enfermeiro", "Enfermeiro")
                        .WithMany("EnfermeiroEspecialidade")
                        .HasForeignKey("EnfermeiroId")
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
#pragma warning restore 612, 618
        }
    }
}
