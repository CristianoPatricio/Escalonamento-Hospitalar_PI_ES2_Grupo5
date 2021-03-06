﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EscalonamentoHospitalar.Models;

namespace EscalonamentoHospitalar.Models
{
    public class HospitalDbContext : DbContext
    {

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {

        }

        public HospitalDbContext()
        {
        }

        public DbSet<UserAccount> userAccount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Chave composta especialidademedicoId + medicoId
            modelBuilder.Entity<MedicoEspecialidade>().HasKey(o => new { o.MedicoId, o.EspecialidadeMedicoId });

            //Relação 1 -> N
            modelBuilder.Entity<MedicoEspecialidade>()
                .HasOne(mm => mm.Medico)
                .WithMany(m => m.MedicosEspecialidade)
                .HasForeignKey(mm => mm.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicoEspecialidade>()
               .HasOne(mm => mm.EspecialidadeMedico)
               .WithMany(m => m.MedicosEspecialidade)
               .HasForeignKey(mm => mm.EspecialidadeMedicoId)
               .OnDelete(DeleteBehavior.ClientSetNull);


            //Chave primária composta
            modelBuilder.Entity<EnfermeiroEspecialidade>().HasKey(o => new { o.EnfermeiroId, o.EspecialidadeEnfermeiroId });

            //Relação 1 -> N
            modelBuilder.Entity<EnfermeiroEspecialidade>()
                .HasOne(ee => ee.Enfermeiro)
                .WithMany(e => e.EnfermeirosEspecialidade)
                .HasForeignKey(ee => ee.EnfermeiroId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EnfermeiroEspecialidade>()
                .HasOne(ee => ee.EspecialidadeEnfermeiro)
                .WithMany(e => e.EnfermeirosEspecialidade)
                .HasForeignKey(ee => ee.EspecialidadeEnfermeiroId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            //Relaçao 1 -> N
            modelBuilder.Entity<Tratamento>()
                .HasOne(ee => ee.Paciente)
                .WithMany(e => e.Tratamentos)
                .HasForeignKey(ee => ee.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

       
        



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EscalonamentoHospitalar.Models.DiretorServico> DiretoresServico { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Medico> Medicos { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Paciente> Pacientes { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Enfermeiro> Enfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.EnfermeiroEspecialidade> EnfermeirosEspecialidades { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.EspecialidadeEnfermeiro> EspecialidadesEnfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Turno> Turnos { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.HorarioEnfermeiro> HorariosEnfermeiro { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.MedicoEspecialidade> MedicoEspecialidades { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.EspecialidadeMedico> EspecialidadeMedicos { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Patologia> Patologia { get; set; }
        public DbSet<EscalonamentoHospitalar.Models.Grau> Grau { get; set; }
        public DbSet<EscalonamentoHospitalar.Models.Regime> Regime { get; set; }
        public DbSet<EscalonamentoHospitalar.Models.Estado> Estado { get; set; }
        public DbSet<EscalonamentoHospitalar.Models.Regra> Regras { get; set; }


        public DbSet<EscalonamentoHospitalar.Models.Tratamento> Tratamentos { get; set; }



        public DbSet<EscalonamentoHospitalar.Models.EstadoPedidoTroca> EstadoPedidoTrocas { get; set; }
        
        public DbSet<EscalonamentoHospitalar.Models.HorarioATrocarEnfermeiro> HorarioATrocarEnfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.HorarioParaTrocaEnfermeiro> HorarioParaTrocaEnfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.PedidoTrocaTurnosEnfermeiro> PedidoTrocaTurnosEnfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.HorarioMedico> HorariosMedicos { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.HorarioATrocarMedico> HorarioATrocarMedico { get; set; }
       
        public DbSet<EscalonamentoHospitalar.Models.HorarioParaTrocaMedico> HorarioParaTrocaMedico { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.PedidoTrocaTurnosMedico> PedidoTrocaTurnosMedico { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.HorarioPaciente> HorariosPaciente { get; set; }


    }
}
