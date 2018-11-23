using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EscalonamentoHospitalar.Models;

namespace EscalonamentoHospitalar.Models
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext (DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

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


            base.OnModelCreating(modelBuilder);
        }

        

        public DbSet<EscalonamentoHospitalar.Models.DiretorServico> DiretorServico { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Enfermeiro> Enfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Medico> Medicos { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Paciente> Pacientes { get; set; }


        public DbSet<EscalonamentoHospitalar.Models.EnfermeiroEspecialidade> EnfermeiroEspecialidades { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.MedicoEspecialidade> MedicoEspecialidades { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.EspecialidadeMedico> EspecialidadeMedicos { get; set; }


    }
}
