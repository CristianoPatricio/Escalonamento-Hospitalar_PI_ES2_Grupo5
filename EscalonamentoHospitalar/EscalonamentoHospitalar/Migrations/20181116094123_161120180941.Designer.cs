﻿// <auto-generated />
using System;
using EscalonamentoHospitalar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EscalonamentoHospitalar.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    [Migration("20181116094123_161120180941")]
    partial class _161120180941
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Contacto");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Morada");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NumeroMecanografico")
                        .IsRequired();

                    b.HasKey("DiretorServicoID");

                    b.ToTable("DiretorServico");
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

            modelBuilder.Entity("EscalonamentoHospitalar.Models.Medico", b =>
                {
                    b.Property<int>("MedicoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CC");

                    b.Property<string>("Contacto");

                    b.Property<DateTime>("Data_Nascimento");

                    b.Property<string>("Email");

                    b.Property<string>("Especialidade");

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

                    b.Property<string>("BoletimClinico");

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

            modelBuilder.Entity("EscalonamentoHospitalar.Models.MedicoEspecialidade", b =>
                {
                    b.HasOne("EscalonamentoHospitalar.Models.Medico", "Medico")
                        .WithMany("MedicoEspecialidade")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
