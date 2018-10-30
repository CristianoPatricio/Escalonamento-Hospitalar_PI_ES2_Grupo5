using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EscalonamentoHospitalar.Models;

namespace EscalonamentoHospitalar.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EscalonamentoHospitalar.Models.DiretorServico> DiretorServico { get; set; }
        public DbSet<EscalonamentoHospitalar.Models.Enfermeiros> Enfermeiros { get; set; }
    }
}
