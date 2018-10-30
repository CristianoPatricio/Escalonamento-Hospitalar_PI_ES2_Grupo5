using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EscalonamentoHospitalar.Models
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext (DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<EscalonamentoHospitalar.Models.DiretorServico> DiretorServico { get; set; }
    }
}
