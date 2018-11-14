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

        public DbSet<EscalonamentoHospitalar.Models.DiretorServico> DiretorServico { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Enfermeiro> Enfermeiros { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Medico> Medicos { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Paciente> Pacientes { get; set; }


        public DbSet<EscalonamentoHospitalar.Models.EnfermeiroEspecialidade> EnfermeiroEspecialidades { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.MedicoEspecialidade> MedicoEspecialidades { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.EspecialidadeMedico> EspecialidadeMedico { get; set; }


    }
}
