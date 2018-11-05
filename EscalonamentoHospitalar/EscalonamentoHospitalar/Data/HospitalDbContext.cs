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

        public DbSet<EscalonamentoHospitalar.Models.Enfermeiro> Enfermeiro { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Medico> Medico { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Paciente> Paciente { get; set; }

        public DbSet<EscalonamentoHospitalar.Models.Tratamento> Tratamento { get; set; }

    }
}
