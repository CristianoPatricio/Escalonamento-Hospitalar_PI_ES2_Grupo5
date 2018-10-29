using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EscalonamentoHospitalar.Models
{
    public class MedicosDbContext : DbContext
    {
        public MedicosDbContext (DbContextOptions<MedicosDbContext> options)
            : base(options)
        {
        }

        public DbSet<EscalonamentoHospitalar.Models.Medicos> Medicos { get; set; }
    }
}
