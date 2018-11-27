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
           
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EscalonamentoHospitalar.Models.DiretorServico> DiretorServico { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Medico> Medicos { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Paciente> Pacientes { get; set; }

<<<<<<< HEAD

        public DbSet<EscalonamentoHospitalar.Models.EnfermeiroEspecialidade> EnfermeiroEspecialidades { get; set; }
=======
        public DbSet<EscalonamentoHospitalar.Models.Enfermeiro> Enfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.EnfermeiroEspecialidade> EnfermeirosEspecialidades { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.EspecialidadeEnfermeiro> EspecialidadesEnfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Turno> Turnos { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.HorarioEnfermeiro> HorariosEnfermeiro { get; set; }
>>>>>>> CristianoPatricio

        public DbSet<EscalonamentoHospitalar.Models.MedicoEspecialidade> MedicoEspecialidade { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Tratamento> Tratamento { get; set; }


    }
}
